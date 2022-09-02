package com.example.apptruyen

import android.app.Application
import android.app.Dialog
import android.app.ProgressDialog
import android.content.Context
import android.icu.text.CaseMap
import android.nfc.Tag

import java.util.*
import android.text.format.DateFormat
import android.util.Log
import android.view.View
import android.widget.ProgressBar
import android.widget.TextView
import android.widget.Toast
import androidx.constraintlayout.helper.widget.MotionEffect.TAG
import com.example.apptruyen.activities.PdfDetailActivity
import com.github.barteksc.pdfviewer.PDFView
import com.google.android.material.tabs.TabLayout
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener
import com.google.firebase.storage.FirebaseStorage
import com.google.firebase.storage.StorageReference
import com.google.firebase.storage.ktx.storageMetadata


class MyApplication:Application() {
    override fun onCreate() {
        super.onCreate()

    }
    companion object{
        fun formatTimeStamp(timestamp: Long):String {
            val cal=Calendar.getInstance(Locale.ENGLISH)
            cal.timeInMillis = timestamp

            return DateFormat.format("dd/MM/yyyy",cal).toString()
        }
        fun loadPdfSize(pdfUrl:String,pdfTitle: String,sizeTv: TextView){
            val TAG="PDF_SIZE_TAG"

            val ref=FirebaseStorage.getInstance().getReferenceFromUrl(pdfUrl)
            ref.metadata
                .addOnSuccessListener { storageMetadata ->
                    Log.d(TAG,"loadPdfSize: got metadata")
                    val bytes=storageMetadata.sizeBytes.toDouble()
                    Log.d(TAG,"loadPdfSize: Size bytes $bytes")

                    val kb=bytes/1024
                    val mb = bytes/1024
                    if(mb>1)
                    {
                        sizeTv.text = "${String.format("%.2f",mb)}MB"
                    }
                    else if(kb >=1)
                    {
                        sizeTv.text = "${String.format("%.2f",kb)}KB"
                    }
                    else
                    {
                        sizeTv.text = "${String.format("%.2f",bytes)}Bytes"
                    }
                }
                .addOnFailureListener { e->
                    Log.d(TAG,"loadPdfSize: Failed to get metadata due to ${e.message}")
                }
        }
        fun loadPdfFromUrlSinglePage(
            pdfUrl: String,
            pdfTitle: String,
            pdfView: PDFView,
            progressBar: ProgressBar,
            pagesTv: TextView?
        ){
            val TAG = "PDF_THUMBNNAIL_TAG"
            val ref=FirebaseStorage.getInstance().getReferenceFromUrl(pdfUrl)
            ref.getBytes(Constants.MAX_BYTES_PDF)
                .addOnSuccessListener { bytes ->

                    Log.d(TAG,"loadPdfSize: Size bytes $bytes")

                    pdfView.fromBytes(bytes)
                        .pages(0)
                        .spacing(0)
                        .swipeHorizontal(false)
                        .enableSwipe(false  )
                        .onError{ t->
                            progressBar.visibility = View.INVISIBLE
                            Log.d(TAG,"loadPdfFromUrlSinglePage: ${t.message}")
                        }
                        .onPageError { page, t ->
                            progressBar.visibility = View.INVISIBLE
                            Log.d(TAG,"loadPdfFromUrlSinglePage: ${t.message}")
                        }
                        .onLoad { nbPages ->
                            Log.d(TAG, "loadPdfFromUrlSinglePage: Pages: $nbPages")
                            progressBar.visibility = View.INVISIBLE

                            if(pagesTv != null)
                            {
                                pagesTv.text = "$nbPages"
                            }
                        }
                        .load()
                }
                .addOnFailureListener { e->
                    Log.d(TAG,"loadPdfSize: Failed to get metadata due to ${e.message}")
                }

        }
        fun loadCategory(categoryId: String,categoryTv : TextView){
            val ref = FirebaseDatabase.getInstance().getReference("Categories")
            ref.child(categoryId)
                .addListenerForSingleValueEvent(object : ValueEventListener{
                    override fun onDataChange(snapshot: DataSnapshot) {
                        val category = "${snapshot.child("category").value}"

                        categoryTv.text = category
                    }

                    override fun onCancelled(error: DatabaseError) {

                    }
                })
        }

        fun deleteBook(context: Context,bookId:String,bookUrl:String,bookTitle:String) {
            val TAG = "DELETE_BOOK_TAG"
            Log.d(TAG, "Đang xoá truyện...")

            val progressDialog = ProgressDialog(context)
            progressDialog.setTitle("Vui lòng đợi")
            progressDialog.setMessage("Đang xoá $bookTitle...")
            progressDialog.setCanceledOnTouchOutside(false)
            progressDialog.show()
            Log.d(TAG, "Xoá truyện: Đang xoá truyện trong thư viện...")
            val storageReference = FirebaseStorage.getInstance().getReferenceFromUrl(bookUrl)
            storageReference.delete()
                .addOnSuccessListener {
                    Log.d(TAG,"Đã xoá truyện trong thư viện...")
                    Log.d(TAG,"Đang xoá truyện trong database...")

                    val ref = FirebaseDatabase.getInstance().getReference("Books")
                    ref.child(bookId)
                        .removeValue()
                        .addOnSuccessListener {
                            progressDialog.dismiss()
                            Toast.makeText(context, "Xoá truyện thành công...", Toast.LENGTH_SHORT).show()
                            Log.d(TAG,"Xoá truyện trong database thành công...")

                        }
                        .addOnFailureListener { e->
                            progressDialog.dismiss()
                            Log.d(TAG,"Xoá truyện thất bại do lỗi ${e.message}")
                            Toast.makeText(context,"Xoá truyện thất bại do lỗi ${e.message}",Toast.LENGTH_SHORT).show()

                        }
                }
                .addOnFailureListener { e->
                    progressDialog.dismiss()
                    Log.d(TAG,"Xoá truyện trong thư viện thất bại do lỗi ${e.message}")
                    Toast.makeText(context,"Xoá truyện thất bại do lỗi ${e.message}",Toast.LENGTH_SHORT).show()
                }
        }

        fun incrementBookViewCount(bookId: String){
            val ref = FirebaseDatabase.getInstance().getReference("Books")
            ref.child(bookId)
                .addListenerForSingleValueEvent(object: ValueEventListener{
                    override fun onDataChange(snapshot: DataSnapshot) {
                        var viewsCount = "${snapshot.child("viewsCount").value}"

                        if (viewsCount==""||viewsCount=="null"){
                            viewsCount="0";
                        }
                        val newViewsCount = viewsCount.toLong()+1

                        val hashMap = HashMap<String, Any>()
                        hashMap["viewsCount"]=newViewsCount

                        val dbRef = FirebaseDatabase.getInstance().getReference("Books")
                        dbRef.child(bookId)
                            .updateChildren(hashMap)
                    }

                    override fun onCancelled(error: DatabaseError) {

                    }
                })
        }

        public fun removeFromFavorite(context: Context,bookId:String){
            val TAG ="Remove Sở thích"
            Log.d(TAG,"xoá khỏi sở thích")

            val firebaseAuth = FirebaseAuth.getInstance()

            val ref = FirebaseDatabase.getInstance().getReference("Users")
            ref.child(firebaseAuth.uid!!).child("Favorites").child(bookId)
                .removeValue()
                .addOnSuccessListener {
                    Log.d(TAG,"Xoá thành công")
                }
                .addOnFailureListener {e->
                    Log.d(TAG,"Xoá thất bại vì ${e.message}")
                    Toast.makeText(context,"Xoá thất bại vì ${e.message}",Toast.LENGTH_SHORT).show()
                }

        }
    }



}