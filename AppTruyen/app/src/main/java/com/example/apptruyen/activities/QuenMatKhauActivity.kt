package com.example.apptruyen.activities

import android.app.ProgressDialog
import android.os.Bundle
import android.os.PersistableBundle
import android.util.Patterns
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.apptruyen.databinding.ActivityQuenMatKhauBinding
import com.google.firebase.auth.FirebaseAuth
import java.util.regex.Pattern

class QuenMatKhauActivity : AppCompatActivity() {

    private  lateinit var binding:ActivityQuenMatKhauBinding

    private lateinit var firebaseAuth: FirebaseAuth

    private lateinit var progressDialog: ProgressDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityQuenMatKhauBinding.inflate(layoutInflater)
        setContentView(binding.root)

        firebaseAuth = FirebaseAuth.getInstance()

        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Đợt Chút")
        progressDialog.setCanceledOnTouchOutside(false)

        binding.backBtn.setOnClickListener {
            onBackPressed()
        }
        binding.submitBtn.setOnClickListener {
            validateData()
        }

    }
    private var email= ""
    private fun validateData() {
        email = binding.emailEt.text.toString().trim()
        if(email.isEmpty())
        {
            Toast.makeText(this,"Hãy nhập Email...",Toast.LENGTH_SHORT).show()
        }
        else if (!Patterns.EMAIL_ADDRESS.matcher(email).matches()){
            Toast.makeText(this,"Email không hợp lệ...",Toast.LENGTH_SHORT).show()
        }
        else
        {
            recoverPassword()
        }
    }

    private fun recoverPassword() {

        progressDialog.setMessage("Đang gửi dữ liệu reset pass của ${email}")
        progressDialog.show()

        firebaseAuth.sendPasswordResetEmail(email)
            .addOnSuccessListener {
                progressDialog.dismiss()
                Toast.makeText(this,"Đã gửi tin nhắn đến email của bạn",Toast.LENGTH_SHORT).show()
            }
            .addOnFailureListener {e->
                progressDialog.dismiss()
                Toast.makeText(this,"Đã gửi tin nhắn đến email thất bại vì {${e.message}",Toast.LENGTH_SHORT).show()
            }
    }
}