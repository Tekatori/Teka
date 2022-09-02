package com.example.apptruyen.activities

import android.app.ProgressDialog
import android.content.Intent
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Environment
import android.util.Log
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.core.content.ContextCompat
import com.example.apptruyen.Constants
import com.example.apptruyen.MyApplication
import com.example.apptruyen.R
import com.example.apptruyen.databinding.ActivityPdfDetailBinding
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener
import com.google.firebase.storage.FirebaseStorage
import java.io.FileOutputStream
import java.lang.Exception

class PdfDetailActivity : AppCompatActivity() {

    private lateinit var binding:ActivityPdfDetailBinding

    private var bookId = ""
    private  var bookTitle=""
    private  var bookUrl=""
    private var isInMyFavorite = false

    private lateinit var firebaseAuth: FirebaseAuth
    private lateinit var progressDialog:ProgressDialog
    private companion object{
        const val TAG ="BOOK_DETAILS_TAG"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPdfDetailBinding.inflate(layoutInflater)
        setContentView(binding.root)

        bookId= intent.getStringExtra("bookId")!!

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Đợi Chút...")
        progressDialog.setCanceledOnTouchOutside(false)

        firebaseAuth = FirebaseAuth.getInstance()
        if(firebaseAuth.currentUser!= null)
        {
            checkIsFavorite()
        }
        MyApplication.incrementBookViewCount(bookId)

        loadBookDetails()

        binding.backBtn.setOnClickListener{
            onBackPressed()
        }
        binding.readBookBtn.setOnClickListener {
            val intent = Intent(this, PdfViewActivity::class.java)
            intent.putExtra("bookId",bookId);
            startActivity(intent)
        }
        binding.downloadBookBtn.setOnClickListener {
            if(ContextCompat.checkSelfPermission(this,android.Manifest.permission.WRITE_EXTERNAL_STORAGE)==PackageManager.PERMISSION_GRANTED)
            {
                Log.d(TAG,"Stogare permission Được Cấp")
                downloadBook()
            }else {
                Log.d(TAG,"Stogare permission Chưa Được Cấp")
                requestStorePermissionLauncher.launch(android.Manifest.permission.WRITE_EXTERNAL_STORAGE)
            }
        }
        binding.favoriteBtn.setOnClickListener {
                if(firebaseAuth.currentUser == null)
                {
                    Toast.makeText(this,"Bạn Phải đăng nhập",Toast.LENGTH_SHORT).show()
                }else
                {
                    if(isInMyFavorite)
                    {
                        MyApplication.removeFromFavorite(this,bookId)
                    }
                    else{
                        addToFavorite()
                    }
                }
        }
    }
    private  val requestStorePermissionLauncher = registerForActivityResult(ActivityResultContracts.RequestPermission()){isGranted: Boolean->
        if(isGranted){
            Log.d(TAG,"Stogare permission Được Cấp")
            downloadBook()
        }else{
            Log.d(TAG,"Stogare permission Chưa Được Cấp")
            Toast.makeText(this,"permission Chưa Được Cấp",Toast.LENGTH_SHORT).show()
        }
    }
    private fun downloadBook(){

        progressDialog.setMessage("Đang Tải Truyện...")
        progressDialog.show()
        val storageReference = FirebaseStorage.getInstance().getReferenceFromUrl(bookUrl)
        storageReference.getBytes(Constants.MAX_BYTES_PDF)
            .addOnSuccessListener {bytes ->
                Log.d(TAG,"Đang Tải Sách...")
                saveToDownFolder(bytes)
            }
            .addOnFailureListener {e->
                progressDialog.dismiss()
                Log.d(TAG,"Tải Sách Thất bại vì ${e.message}")
                Toast.makeText(this,"Tải Sách Thất bại vì ${e.message}",Toast.LENGTH_SHORT).show()
            }
    }

    private fun saveToDownFolder(bytes: ByteArray?) {
        val nameWithExtention = "${System.currentTimeMillis()}.pdf"

        try {
            val downloadFolder = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DOWNLOADS)
            downloadFolder.mkdirs()
            val filePath = downloadFolder.path+"/"+nameWithExtention
            val out = FileOutputStream(filePath)
            out.write(bytes)
            out.close()

            Toast.makeText(this,"Đã Lưu vào thư mục",Toast.LENGTH_SHORT).show()
            Log.d(TAG,"Đã Lưu vào thư mục")
            progressDialog.dismiss()
            incrementDownloadCount()
        }
        catch (e: Exception){
            progressDialog.dismiss()
            Log.d(TAG,"Lưu vào thư mục thất bài vì ${e.message}")
            Toast.makeText(this,"Lưu vào thư mục thất bại vì ${e.message}",Toast.LENGTH_SHORT).show()

        }
    }
    private fun incrementDownloadCount(){
        Log.d(TAG,"incrementDownloadCount:")

        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .addListenerForSingleValueEvent(object :ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    var downloadsCount ="${snapshot.child("downloadsCount").value}"
                    Log.d(TAG,"Thời gian tải truyện: $downloadsCount ")

                    if(downloadsCount==""||downloadsCount=="null"){
                        downloadsCount ="0"
                    }
                    val newDownloadCount: Long = downloadsCount.toLong() + 1
                    Log.d(TAG,"New Downloads Count :$newDownloadCount ")

                    val hashMap:HashMap<String,Any> = HashMap()
                    hashMap["downloadsCount"] = newDownloadCount

                    val dbref = FirebaseDatabase.getInstance().getReference("Books")
                    dbref.child(bookId)
                        .updateChildren(hashMap)
                        .addOnSuccessListener {
                            Log.d(TAG,"Lượt Tải Tăng")
                        }
                        .addOnFailureListener {e->
                            Log.d(TAG,"thất bại vì ${e.message}")
                        }
                }

                override fun onCancelled(error: DatabaseError) {
                    TODO("Not yet implemented")
                }
            })
    }
    private fun loadBookDetails() {
        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .addListenerForSingleValueEvent(object: ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {

                    val categoryId = "${snapshot.child("categoryId").value}"
                    val description = "${snapshot.child("description").value}"
                    val downloadsCount = "${snapshot.child("downloadsCount").value}"
                    val timestamp = "${snapshot.child("timestamp").value}"
                    bookTitle = "${snapshot.child("title").value}"
                    val uid = "${snapshot.child("uid").value}"
                    bookUrl = "${snapshot.child("url").value}"
                    val viewsCount = "${snapshot.child("viewsCount").value}"

                    val date = MyApplication.formatTimeStamp(timestamp.toLong())

                    MyApplication.loadCategory(categoryId, binding.categoryTv)

                    MyApplication.loadPdfFromUrlSinglePage(
                        "$bookUrl",
                        "$bookTitle",
                        binding.pdfView,
                        binding.progressBar,
                        binding.pagesTv
                    )

                    MyApplication.loadPdfSize("$bookUrl", "$bookTitle", binding.sizeTv)

                    binding.titleTv.text = bookTitle
                    binding.descriptionTv.text = description
                    binding.viewsTv.text = viewsCount
                    binding.downloadsTv.text = downloadsCount
                    binding.dateTv.text = date

                }

                override fun onCancelled(error: DatabaseError) {

                }
            })
    }
    private fun checkIsFavorite(){
        Log.d(TAG,"Check truyện đã được add vào sở thích chưa")
        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(firebaseAuth.uid!!).child("Favorites").child(bookId)
            .addValueEventListener(object :ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    isInMyFavorite = snapshot.exists()
                    if(isInMyFavorite)
                    {

                        binding.favoriteBtn.setCompoundDrawablesRelativeWithIntrinsicBounds(0,
                            R.drawable.ic_favorite_border_white,0,0)
                        binding.favoriteBtn.text="Bỏ yêu thích"
                    }
                    else
                    {
                        binding.favoriteBtn.setCompoundDrawablesRelativeWithIntrinsicBounds(0,
                            R.drawable.ic_favorite_border_white,0,0)
                        binding.favoriteBtn.text="yêu thích"
                    }

                }

                override fun onCancelled(error: DatabaseError) {
                    TODO("Not yet implemented")
                }
            })


    }
    private fun addToFavorite(){
        Log.d(TAG,"thêm vào sở thích")
        val timestamp = System.currentTimeMillis()

        val hashMap = HashMap<String,Any>()
        hashMap["bookId"] = bookId;
        hashMap["timestamp"] = timestamp

        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(firebaseAuth.uid!!).child("Favorites").child(bookId)
            .setValue(hashMap)
            .addOnSuccessListener {
                Log.d(TAG,"thêm thành công")
            }
            .addOnFailureListener {e->
                Log.d(TAG,"thêm thất bại vì ${e.message}")
                Toast.makeText(this,"thêm thất bại vì ${e.message}",Toast.LENGTH_SHORT).show()
            }
    }


}