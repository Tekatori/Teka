package com.example.apptruyen.activities

import android.app.ProgressDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Patterns
import android.widget.Toast
import com.example.apptruyen.databinding.ActivityDangNhapBinding
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener

class DangNhapActivity : AppCompatActivity() {

    private lateinit var binding: ActivityDangNhapBinding

    private lateinit var firebaseAuth: FirebaseAuth

    private  lateinit var progressDialog: ProgressDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityDangNhapBinding.inflate(layoutInflater)
        setContentView(binding.root)

        firebaseAuth = FirebaseAuth.getInstance()

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Đợt Chút!")
        progressDialog.setCanceledOnTouchOutside(false)

        binding.noAccountTv.setOnClickListener {
            startActivity(Intent(this, DangKyActivity::class.java))
        }

        binding.loginBtn.setOnClickListener {
            validateData()
        }
        binding.forgotTv.setOnClickListener {
            startActivity(Intent(this,QuenMatKhauActivity::class.java))
        }

    }
    private var  email=""
    private  var password = ""

    private fun validateData(){
        email = binding.email.text.toString().trim()
        password = binding.password.text.toString().trim()
        if(!Patterns.EMAIL_ADDRESS.matcher(email).matches())
        {
            Toast.makeText(this,"Email Không Đúng...",Toast.LENGTH_SHORT).show()
        }
        else if(password.isEmpty()) {
            Toast.makeText(this,"Chưa Nhập Password...",Toast.LENGTH_SHORT).show()
        }
        else
        {
            loginUser()
        }
        }
    private fun loginUser(){

        progressDialog.setMessage("Đang đăng nhập...")
        progressDialog.show()

        firebaseAuth.signInWithEmailAndPassword(email, password)
            .addOnSuccessListener {
                checkUser()
            }
            .addOnFailureListener {
                Toast.makeText(this,"Đăng nhập thất bại..",Toast.LENGTH_SHORT).show()
            }
    }
    private  fun checkUser(){
        progressDialog.setMessage("Đang Kiểm Tra...")

        val firebaseUser = firebaseAuth.currentUser!!

        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(firebaseUser.uid)
            .addListenerForSingleValueEvent(object : ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    progressDialog.dismiss()
                    val userType =snapshot.child("userType").value
                    if(userType=="user")
                    {
                        startActivity(Intent(this@DangNhapActivity, TrangUserActivity::class.java))
                        finish()
                    }
                    else if(userType=="admin")
                    {
                        startActivity(Intent(this@DangNhapActivity, TrangAdminActivity::class.java))
                        finish()
                    }
                }


                override fun onCancelled(error: DatabaseError) {

                }
            })



    }

}