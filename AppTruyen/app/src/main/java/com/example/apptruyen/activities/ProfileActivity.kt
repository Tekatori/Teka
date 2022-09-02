package com.example.apptruyen.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.bumptech.glide.Glide
import com.example.apptruyen.MyApplication
import com.example.apptruyen.R
import com.example.apptruyen.adapters.AdapterPdfFavorite
import com.example.apptruyen.databinding.ActivityProfileBinding
import com.example.apptruyen.models.ModelPdf
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener
import java.lang.Exception

class ProfileActivity : AppCompatActivity() {

    private lateinit var binding: ActivityProfileBinding

    private lateinit var firebaseAuth: FirebaseAuth

    private lateinit var bookArrayList: ArrayList<ModelPdf>

    private lateinit var adapterPdfFavorite: AdapterPdfFavorite

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityProfileBinding.inflate(layoutInflater)
        setContentView(binding.root)

        firebaseAuth = FirebaseAuth.getInstance()
        loadUserInfo()
        loadFavoriteBooks()

        binding.backBtn.setOnClickListener{
            onBackPressed()
        }

        binding.profileEditBtn.setOnClickListener{
            startActivity(Intent(this, ProfileEditActivity::class.java))
        }
    }
    private fun loadUserInfo(){
        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(firebaseAuth.uid!!)
            .addValueEventListener(object: ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    val email= "${snapshot.child("email").value}"
                    val name= "${snapshot.child("name").value}"
                    val profileImage= "${snapshot.child("profileImage").value}"
                    val timestamp= "${snapshot.child("timestamp").value}"
                    val uid =  "${snapshot.child("uid").value}"
                    val userType= "${snapshot.child("userType").value}"

                    val formattedDate = MyApplication.formatTimeStamp(timestamp.toLong())

                    binding.nameTv.text = name
                    binding.emailTv.text=email
                    binding.memberDateTv.text=formattedDate
                    binding.accountTypeTv.text=userType

                    try {
                        Glide.with(this@ProfileActivity)
                            .load(profileImage)
                            .placeholder(R.drawable.ic_person_gray)
                            .into(binding.profileIv)
                    }
                    catch (e: Exception){

                    }

                }

                override fun onCancelled(error: DatabaseError) {

                }
            })
    }

    private fun loadFavoriteBooks(){
        bookArrayList = ArrayList();

        val ref = FirebaseDatabase.getInstance().getReference("Users")
        ref.child(firebaseAuth.uid!!).child("Favorites")
            .addValueEventListener(object :ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    bookArrayList.clear()

                    for(ds in snapshot.children){
                        val bookId ="${ds.child("bookId").value}"
                        val modelPdf = ModelPdf()
                        modelPdf.id = bookId

                        bookArrayList.add(modelPdf)
                    }
                    binding.favoriteBookCountTv.text = "${bookArrayList.size}"
                    adapterPdfFavorite = AdapterPdfFavorite(this@ProfileActivity,bookArrayList)
                    binding.favoriteRv.adapter = adapterPdfFavorite

                }

                override fun onCancelled(error: DatabaseError) {

                }
            })
    }
}