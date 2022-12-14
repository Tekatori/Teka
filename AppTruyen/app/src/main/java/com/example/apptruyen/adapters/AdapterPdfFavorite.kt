package com.example.apptruyen.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.appcompat.view.menu.ActionMenuItemView
import androidx.recyclerview.widget.RecyclerView
import com.example.apptruyen.databinding.RowPdfFavoriteBinding
import com.example.apptruyen.models.ModelPdf
import android.content.Context
import android.content.Intent
import com.example.apptruyen.MyApplication
import com.example.apptruyen.activities.PdfDetailActivity
import com.google.firebase.database.DataSnapshot
import com.google.firebase.database.DatabaseError
import com.google.firebase.database.FirebaseDatabase
import com.google.firebase.database.ValueEventListener

class AdapterPdfFavorite :RecyclerView.Adapter<AdapterPdfFavorite.HolderPdfFavorite>{

    private val context:Context

    private var bookArrayList : ArrayList<ModelPdf>

    private lateinit var binding:RowPdfFavoriteBinding

    constructor(context: Context, bookArrayList: ArrayList<ModelPdf>) {
        this.context = context
        this.bookArrayList = bookArrayList
    }
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): HolderPdfFavorite {
        binding = RowPdfFavoriteBinding.inflate(LayoutInflater.from(context),parent,false)

        return HolderPdfFavorite(binding.root)
    }

    override fun onBindViewHolder(holder: HolderPdfFavorite, position: Int) {
       val model = bookArrayList[position]

        loadBookDetails(model,holder)

        holder.itemView.setOnClickListener{
            val intent = Intent(context,PdfDetailActivity::class.java)
            intent.putExtra("bookId",model.id)
            context.startActivity(intent)
        }

        holder.removeFavBtn.setOnClickListener {
            MyApplication.removeFromFavorite(context,model.id)
        }
    }

    private fun loadBookDetails(model: ModelPdf, holder: AdapterPdfFavorite.HolderPdfFavorite) {
        val bookId = model.id

        val ref = FirebaseDatabase.getInstance().getReference("Books")
        ref.child(bookId)
            .addListenerForSingleValueEvent(object :ValueEventListener{
                override fun onDataChange(snapshot: DataSnapshot) {
                    val categoryId = "${snapshot.child("categoryId").value}"
                    val description = "${snapshot.child("description").value}"
                    val downloadsCount = "${snapshot.child("downloadsCount").value}"
                    val timestamp = "${snapshot.child("timestamp").value}"
                    val title = "${snapshot.child("title").value}"
                    val uid = "${snapshot.child("uid").value}"
                    val url = "${snapshot.child("url").value}"
                    val viewsCount = "${snapshot.child("viewsCount").value}"

                    model.isFavorite = true
                    model.title = title
                    model.description = description
                    model.categoryId = categoryId
                    model.timestamp = timestamp.toLong()
                    model.uid = uid
                    model.url = url
                    model.viewsCount = viewsCount.toLong()
                    model.downloadsCount = downloadsCount.toLong()

                    val date = MyApplication.formatTimeStamp(timestamp.toLong())
                    MyApplication.loadCategory("$categoryId",holder.categoryTv)
                    MyApplication.loadPdfFromUrlSinglePage("$url","title",holder.pdfView,holder.progressBar,null);
                    MyApplication.loadPdfSize("$url","title",holder.sizeTv)

                    holder.titleTV.text = title
                    holder.descriptionTv.text = description
                    holder.dateTv.text = date

                }

                override fun onCancelled(error: DatabaseError) {

                }
            })
    }

    override fun getItemCount(): Int {
        return bookArrayList.size
    }




    inner class HolderPdfFavorite(itemView: View) : RecyclerView.ViewHolder(itemView){
        var pdfView = binding.pdfView
        var progressBar = binding.progressBar
        var titleTV = binding.titleTv
        var removeFavBtn = binding.removeFavBtn
        var categoryTv = binding.categoryTv
        var descriptionTv = binding.descriptionTv
        var sizeTv = binding.sizeTv
        var dateTv = binding.dateTv

    }


}