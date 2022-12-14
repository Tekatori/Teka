package com.example.apptruyen.activities

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import com.example.apptruyen.Constants
import com.example.apptruyen.databinding.ActivityPdfViewBinding
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener
import com.google.firebase.storage.FirebaseStorage

class PdfViewActivity : AppCompatActivity() {

    private lateinit var binding: ActivityPdfViewBinding
    var bookId=""
    private companion object{
        const val TAG="PDF_VIEW_TAG"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPdfViewBinding.inflate(layoutInflater)
        setContentView(binding.root)

        bookId=intent.getStringExtra("bookId")!!
        loadBookDetails()

        binding.backBtn.setOnClickListener {
            onBackPressed()
        }
    }

    private fun loadBookDetails() {
        Log.d(TAG,"Đang lấy file PDF từ database")
        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .addListenerForSingleValueEvent(object :ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    val pdfUrl = snapshot.child("url").value
                    Log.d(TAG,"PDF_URL : $pdfUrl")

                    loadBookFromUrl("$pdfUrl")
                }

                override fun onCancelled(error: DatabaseError) {
                    TODO("Not yet implemented")
                }
            })
    }

    private fun loadBookFromUrl(pdfUrl: String) {
        Log.d(TAG,"Đang lấy file PDF từ database")
        val reference = FirebaseStorage.getInstance().getReferenceFromUrl(pdfUrl)
        reference.getBytes(Constants.MAX_BYTES_PDF)
            .addOnSuccessListener {bytes->
                Log.d(TAG,"Đang lấy file PDF từ URL")

                binding.pdfView.fromBytes(bytes)
                    .swipeHorizontal(false)
                    .onPageChange{page,pageCount->
                        val currentPage = page+ 1
                        binding.toolbarSubTitle.text = "$currentPage/$pageCount"
                        Log.d(TAG,"Đang lấy Truyện từ Url $currentPage/$pageCount ")
                    }
                    .onError{t->
                        Log.d(TAG,"Đang lấy Truyện từ Url lỗi vì ${t.message}")
                    }
                    .onPageError{page,t->
                        Log.d(TAG,"Đang lấy Truyện từ Url lỗi vì ${t.message}")
                    }
                    .load()
                binding.progressBar.visibility = View.GONE
            }
            .addOnFailureListener {e->
                Log.d(TAG,"Đang lấy file PDF từ Url thất bại vì : ${e.message}")
                binding.progressBar.visibility = View.GONE
            }

    }
}