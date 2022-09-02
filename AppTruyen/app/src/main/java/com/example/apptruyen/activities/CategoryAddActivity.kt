package com.example.apptruyen.activities

import android.app.ProgressDialog
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.example.apptruyen.databinding.ActivityCategoryAddBinding
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.FirebaseDatabase

class CategoryAddActivity : AppCompatActivity() {

    private lateinit var binding: ActivityCategoryAddBinding

    private  lateinit var firebaseAuth: FirebaseAuth

    private lateinit var progressDialog: ProgressDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityCategoryAddBinding.inflate(layoutInflater)
        setContentView(binding.root)

        firebaseAuth =  FirebaseAuth.getInstance()

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Chờ xíu....")
        progressDialog.setCanceledOnTouchOutside(false)

        binding.backBtn.setOnClickListener {
            onBackPressed()
        }
        binding.submitBtn.setOnClickListener {
            ValidateData()
        }


    }
    private var category=""
    private fun ValidateData()
    {
        category=binding.categoryEt.text.toString().trim()

        if(category.isEmpty())
        {
            Toast.makeText(this,"Hãy nhập thể loại...", Toast.LENGTH_SHORT).show()
        }
        else {
            addCategoryFirebase()
        }
    }
    private fun addCategoryFirebase()
    {
        progressDialog.show()
        val timestamp = System.currentTimeMillis()

        val hashMap = HashMap<String,Any>()
        hashMap["id"]="$timestamp"
        hashMap["category"]=category
        hashMap["timestamp"]=timestamp
        hashMap["uid"]="${firebaseAuth.uid}"

        val ref= FirebaseDatabase.getInstance().getReference("Categories")
        ref.child("$timestamp")
            .setValue(hashMap)
            .addOnSuccessListener {
                Toast.makeText(this,"Thêm Thành công..", Toast.LENGTH_SHORT).show()
                finish()
            }
            .addOnFailureListener {
                progressDialog.dismiss()
                Toast.makeText(this,"Thêm Thất bại..", Toast.LENGTH_SHORT).show()
            }


    }
}