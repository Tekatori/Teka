package com.example.apptruyen

import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.EditText
import com.example.apptruyen.adapters.AdapterPdfUser
import com.example.apptruyen.databinding.FragmentBooksUserBinding
import com.example.apptruyen.models.ModelPdf
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener


class BooksUserFragment : Fragment {

    // View binding fragment_books_user.xml ==> FragmentBooksUserBinding
    private lateinit var binding: FragmentBooksUserBinding

    public companion object{
        private const val TAG = "BOOK_USER_TAG"

        //Nhan du lieu trang activity load Sach
        public fun newInstance(categoryId: String, category: String, uid: String): BooksUserFragment{
            val fragment= BooksUserFragment()
            //lay du lie tu Bundel Intent
            val args = Bundle()
            args.putString("categoryId", categoryId)
            args.putString("category", category)
            args.putString("uid", uid)
            fragment.arguments = args
            return fragment
        }
    }

    private var categoryId =""
    private var category =""
    private var uid = ""

    //ArrayList de lay pdfs
    private lateinit var pdfArrayList: ArrayList<ModelPdf>
    private lateinit var adapterPdfUser: AdapterPdfUser

    constructor()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //set argument passed in newInstance method

        val args = arguments
        if(args!=null)
        {
            categoryId = args.getString("categoryId")!!
            category = args.getString("category")!!
            uid = args.getString("uid")!!
        }
    }


    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        // Inflate the layout for this fragment
        binding = FragmentBooksUserBinding.inflate(LayoutInflater.from(context), container, false)

        //load pdf thanh cac danh muc

        Log.d(TAG, "onCreateView: Category: $category")
        if(category == "All"){
            //Load tat ca cac sach
            loadAllBooks()
        }
        else if(category == "Most Viewed"){
            //Load sach doc nhieu nhat
            loadMostViewedDownloadedBooks("viewsCount")
        }
        else if(category == "Most Downloaded"){
            //Load sach tai nhieu nhat
            loadMostViewedDownloadedBooks("downloadsCount")
        }
        else
        {
            loadCategorizedBooks()
        }


        //search

        binding.searchEt.addTextChangedListener{ object :TextWatcher{
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {

            }

            override fun onTextChanged(s: CharSequence?, p1: Int, p2: Int, p3: Int) {
                try{
                    adapterPdfUser.filter.filter(s)
                }
                catch (e: Exception){
                    Log.d(TAG, "onTextChanged: SEARCH EXCEPTION: ${e.message}")
                }
            }

            override fun afterTextChanged(p0: Editable?) {

            }
        }}

        return binding.root
    }
    private fun EditText.addTextChangedListener(function: () -> TextWatcher) {

    }

    private fun loadAllBooks() {
        pdfArrayList  = ArrayList()
        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.addValueEventListener(object : ValueEventListener{
            override fun onDataChange(snapshot: DataSnapshot) {
                //Clear danh sach truox khi them vao
                pdfArrayList.clear()
                for(ds in snapshot.children)
                {
                    //lay du lieu
                    val model = ds.getValue(ModelPdf::class.java)

                    // them vao danh sach
                    pdfArrayList.add(model!!)
                }
                adapterPdfUser= AdapterPdfUser(context!!, pdfArrayList)

                //set adapter to recyclerview
                binding.booksRv.adapter = adapterPdfUser
            }

            override fun onCancelled(error: DatabaseError) {
            }
        })
    }

    private fun loadMostViewedDownloadedBooks(orderBy: String) {
        pdfArrayList  = ArrayList()
        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.orderByChild(orderBy).limitToLast(10) // load ds sach xem nhieu hoac tai nhieu nhat
            .addValueEventListener(object : ValueEventListener{
            override fun onDataChange(snapshot: DataSnapshot) {
                //Clear danh sach truox khi them vao
                pdfArrayList.clear()
                for(ds in snapshot.children)
                {
                    //lay du lieu
                    val model = ds.getValue(ModelPdf::class.java)

                    // them vao danh sach
                    pdfArrayList.add(model!!)
                }
                adapterPdfUser= AdapterPdfUser(context!!, pdfArrayList)

                //set adapter to recyclerview
                binding.booksRv.adapter = adapterPdfUser
            }

            override fun onCancelled(error: DatabaseError) {
            }
        })
    }

    private fun loadCategorizedBooks() {
        pdfArrayList  = ArrayList()
        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.orderByChild("categoryId") .equalTo(categoryId)
            .addValueEventListener(object : ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    //Clear danh sach truox khi them vao
                    pdfArrayList.clear()
                    for(ds in snapshot.children)
                    {
                        //lay du lieu
                        val model = ds.getValue(ModelPdf::class.java)

                        // them vao danh sach
                        pdfArrayList.add(model!!)
                    }
                    adapterPdfUser= AdapterPdfUser(context!!, pdfArrayList)

                    //set adapter to recyclerview
                    binding.booksRv.adapter = adapterPdfUser
                }

                override fun onCancelled(error: DatabaseError) {
                }
            })
    }

}



