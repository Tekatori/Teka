package com.example.apptruyen.adapters

import android.app.AlertDialog
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.apptruyen.databinding.RowCategoryBinding
import android.content.Context
import android.content.Intent
import android.widget.*
import com.google.firebase.database.FirebaseDatabase
import android.widget.Filterable
import com.example.apptruyen.filters.FilterCategory
import com.example.apptruyen.models.ModelCategory
import com.example.apptruyen.activities.PdfListAdminActivity

class AdapterCategory:RecyclerView.Adapter<AdapterCategory.HolderCategory>,Filterable{

    private val context: Context
    public var categoryArrayList: ArrayList<ModelCategory>
    private var filterList:ArrayList<ModelCategory>

    private var filter: FilterCategory? = null

    private lateinit var binding:RowCategoryBinding

    constructor(context: Context, categoryArrayList: ArrayList<ModelCategory>) {
        this.context = context
        this.categoryArrayList = categoryArrayList
        this.filterList = categoryArrayList
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): HolderCategory {
        binding = RowCategoryBinding.inflate(LayoutInflater.from(context),parent,false)

        return HolderCategory(binding.root)
    }

    override fun onBindViewHolder(holder: HolderCategory, position: Int) {
        val model = categoryArrayList[position]
        val id=model.id
        val category=model.category
        val uid=model.uid
        val timestamp=model.timestamp

        holder.categoryTv.text=category

        holder.deleteBtn.setOnClickListener{
            val builder = AlertDialog.Builder(context)
            builder.setTitle("Xóa")
                .setMessage("Xóa thể loại này?")
                .setPositiveButton("Xác nhận"){a,d->
                    Toast.makeText(context, "Đang xóa...", Toast.LENGTH_SHORT).show()
                    deleteCategory(model, holder)
                }
                .setNegativeButton("Hủy"){a,d->
                    a.dismiss()
                }
                .show()
        }
        holder.itemView.setOnClickListener {
            val intent = Intent(context, PdfListAdminActivity::class.java)
            intent.putExtra("categoryId",id)
            intent.putExtra("category",category)
            context.startActivity(intent)
        }
    }

    override fun getItemCount(): Int {
        return categoryArrayList.size
    }




    inner class HolderCategory(itemView: View): RecyclerView.ViewHolder(itemView){

        var categoryTv:TextView = binding.categoryTv
        var deleteBtn:ImageButton=binding.deleteBtn
    }


    private fun deleteCategory(model: ModelCategory, holder: HolderCategory)
    {
        val id = model.id
        val ref= FirebaseDatabase.getInstance().getReference("Categories")
        ref.child(id)
            .removeValue()
            .addOnSuccessListener {
                Toast.makeText(context, "Đã xóa....", Toast.LENGTH_SHORT).show()
            }
            .addOnFailureListener{

            }
    }
    override fun getFilter():Filter{
        if (filter==null){
            filter = FilterCategory(filterList, this)
        }
        return filter as FilterCategory
    }
}