package com.example.apptruyen.activities

import android.app.ProgressDialog
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import com.example.apptruyen.databinding.ActivityPdfEditBinding
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener

class PdfEditActivity : AppCompatActivity() {

    private lateinit var binding: ActivityPdfEditBinding
    private var bookId=""
    private lateinit var progressDialog: ProgressDialog
    private lateinit var categoryTitleArrayList : ArrayList<String>
    private lateinit var categoryIdArrayList:ArrayList<String>
    private companion object{
        private const val TAG ="PDF_EDIT_TAG"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPdfEditBinding.inflate(layoutInflater)
        setContentView(binding.root)

        bookId = intent.getStringExtra("bookId")!!

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Đợi Chút")
        progressDialog.setCanceledOnTouchOutside(false)

        loadCategories()
        loadBookInfo()

        binding.backBtn.setOnClickListener {
            onBackPressed()
        }
        binding.categoryTv.setOnClickListener {
            categoryDialog()
        }
        binding.submitBtn.setOnClickListener {
            validateData()
        }
    }

    private fun loadBookInfo() {
        Log.d(TAG,"Load thông tin truyện")

        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .addListenerForSingleValueEvent(object : ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    selectedCategoryId = snapshot.child("categoryId").value.toString()
                    val description = snapshot.child("description").value.toString()
                    val title = snapshot.child("title").value.toString()
                    binding.titleEt.setText(title)
                    binding.descriptionEt.setText(description)

                    Log.d(TAG,"Load thông tin thể loại truyện")
                    val refBookCategory = FirebaseDatabase.getInstance().getReference("Categories")
                    refBookCategory.child(selectedCategoryId)
                        .addListenerForSingleValueEvent(object :ValueEventListener{
                            override fun onDataChange(snapshot: DataSnapshot) {
                                val category = snapshot.child("category").value
                                binding.categoryTv.text=category.toString()
                            }

                            override fun onCancelled(error: DatabaseError) {

                            }
                        })
                }
                override fun onCancelled(error: DatabaseError) {

                }

            })
    }

    private var title=""
    private var description=""
    private fun validateData() {
        title = binding.titleEt.text.toString().trim()
        description = binding.descriptionEt.text.toString().trim()

        if(title.isEmpty()){
            Toast.makeText(this,"nhập Tên Truyện",Toast.LENGTH_SHORT).show()
        }else if(description.isEmpty())
        {
            Toast.makeText(this,"nhập Mô Tả",Toast.LENGTH_SHORT).show()
        }else if(selectedCategoryId.isEmpty()){
            Toast.makeText(this,"Chưa Chọn Thể Loại",Toast.LENGTH_SHORT).show()
        }else{
            updatepdf()
        }
    }

    private fun updatepdf() {
        Log.d(TAG,"UpdatePdf: Bắt Đầu Update thông tin")
        progressDialog.setMessage("Đang Update ...")
        progressDialog.show()

        val hashMap = HashMap<String,Any>()
        hashMap["title"] ="$title"
        hashMap["description"]="$description"
        hashMap["categoryId"] ="$selectedCategoryId"

        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .updateChildren(hashMap)
            .addOnSuccessListener {
                progressDialog.dismiss()
                Log.d(TAG,"UpdatePdf:  Update Thành Công")
                Toast.makeText(this,"Update Thành Công...",Toast.LENGTH_SHORT).show()
            }
            .addOnFailureListener { e->
                Log.d(TAG,"UpdatePdf:  Update Thất Bại vì ${e.message}")
                progressDialog.dismiss()
                Toast.makeText(this,"Update Thất Bại vì ${e.message}.",Toast.LENGTH_SHORT).show()
            }
    }

    private var selectedCategoryId=""
    private var selectedCategoryTitle=""
    private fun categoryDialog() {
        val categoriesArray = arrayOfNulls<String>(categoryTitleArrayList.size)
        for(i in categoryTitleArrayList.indices){
            categoriesArray[i]=categoryTitleArrayList[i]
        }
        val builder = android.app.AlertDialog.Builder(this)
        builder.setTitle("Chọn thể loại")
            .setItems(categoriesArray){dialog,position->

                selectedCategoryId = categoryIdArrayList[position]
                selectedCategoryTitle = categoryTitleArrayList[position]
                binding.categoryTv.text = selectedCategoryTitle

            }
            .show()
    }

    private fun loadCategories() {
        Log.d(TAG,"loadCategories: Đang load thể loại...")

        categoryIdArrayList = ArrayList()
        categoryTitleArrayList = ArrayList()
        val ref = FirebaseDatabase.getInstance().getReference("Categories")
        ref.addListenerForSingleValueEvent(object:ValueEventListener{
            override fun onDataChange(snapshot: DataSnapshot) {
                categoryIdArrayList.clear()
                categoryTitleArrayList.clear()
                for(ds in snapshot.children) {

                    val id ="${ds.child("id").value}"
                    val category = "${ds.child("category").value}"

                    categoryIdArrayList.add(id)
                    categoryTitleArrayList.add(category)
                    Log.d(TAG,"OnDataChange: Category ID $id ")
                    Log.d(TAG,"OnDataChange: Category ID $category ")
                }


            }

            override fun onCancelled(error: DatabaseError) {

            }
        })
    }
}