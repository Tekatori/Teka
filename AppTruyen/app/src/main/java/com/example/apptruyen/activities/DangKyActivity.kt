package com.example.apptruyen.activities

import android.app.ProgressDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle

import android.util.Patterns
import android.widget.Toast
import com.example.apptruyen.databinding.ActivityDangKyBinding
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.FirebaseDatabase

class DangKyActivity : AppCompatActivity() {


    private lateinit var binding: ActivityDangKyBinding

    private lateinit var firebaseAuth: FirebaseAuth

    private  lateinit var progressDialog: ProgressDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityDangKyBinding.inflate(layoutInflater)
        setContentView(binding.root)

        firebaseAuth = FirebaseAuth.getInstance()

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Đợt Chút!")
        progressDialog.setCanceledOnTouchOutside(false)

        binding.backBtn.setOnClickListener {
            onBackPressed()
        }


        binding.registerBtn.setOnClickListener {
            validateData()
        }

    }
    private var name="";
    private var  email = "";
    private var password="";

    private fun validateData(){
        name = binding.nameEt.text.toString().trim()
        email = binding.emailEt.text.toString().trim()
        password = binding.passwordEt.text.toString().trim()
        val cPassword = binding.cPasswordEt.text.toString().trim()

        if(name.isEmpty()){

            Toast.makeText(this,"Hãy nhập tên đăng nhập...",Toast.LENGTH_SHORT).show()
        }
        else if(!Patterns.EMAIL_ADDRESS.matcher(email).matches()){
            Toast.makeText(this,"Hãy nhập đúng email...",Toast.LENGTH_SHORT).show()
        }
        else if(password.isEmpty()){
            Toast.makeText(this,"Hãy nhập Password...",Toast.LENGTH_SHORT).show()
        }
        else if(cPassword.isEmpty()) {
            Toast.makeText(this,"Hãy nhập lại Password...",Toast.LENGTH_SHORT).show()
        }
        else if(password!= cPassword)
        {
            Toast.makeText(this,"Password không trùng..",Toast.LENGTH_SHORT).show()
        }
        else
        {
            createUserAccount()
        }

    }
    private fun createUserAccount(){
        progressDialog.setMessage("Đang tạo tài khoản...")
        progressDialog.show()

        firebaseAuth.createUserWithEmailAndPassword(email, password)
            .addOnSuccessListener {
                updateUserInfo()
            }
            .addOnFailureListener {
                progressDialog.dismiss()
                Toast.makeText(this,"Tạo thất bại...",Toast.LENGTH_SHORT).show()
            }

    }
    private  fun updateUserInfo(){
        progressDialog.setMessage("Đang lưu...")

        val timestamp = System.currentTimeMillis()

        val uid = firebaseAuth.uid

        val hashmap: HashMap<String,Any?> = HashMap()
        hashmap["uid"] = uid;
        hashmap["email"] = email;
        hashmap["name"] = name;
        hashmap["image"] = "";
        hashmap["userType"] = "user";
        hashmap["timestamp"] = timestamp;

        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(uid!!)
            .setValue(hashmap)
            .addOnSuccessListener {
                progressDialog.dismiss()
                Toast.makeText(this,"Đang tạo...",Toast.LENGTH_SHORT).show()
                startActivity(Intent(this@DangKyActivity, TrangUserActivity::class.java))
                finish()
            }
            .addOnFailureListener {
                progressDialog.dismiss()
                Toast.makeText(this,"Tạo thất bại...",Toast.LENGTH_SHORT).show()
            }





    }
}