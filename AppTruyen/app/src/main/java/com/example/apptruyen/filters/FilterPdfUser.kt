package com.example.apptruyen.filters

import android.widget.Filter
import com.example.apptruyen.adapters.AdapterPdfUser
import com.example.apptruyen.models.ModelPdf

class FilterPdfUser: Filter {
    //Tao Mot ArrayList Filter de tim kiem
    var filterList: ArrayList<ModelPdf>

    //Adapter can phai co de filter thuc hien

    var adapterPdfUser: AdapterPdfUser

    //Constructor
    constructor(filterList: ArrayList<ModelPdf>, adapterPdfUser: AdapterPdfUser): super() {
        this.filterList = filterList
        this.adapterPdfUser = adapterPdfUser
    }

    override fun performFiltering(constraint: CharSequence): FilterResults {
        var constraint: CharSequence? = constraint

        val results = FilterResults()
        // gia tri khong the null hoac rong

        if(constraint != null && constraint.isEmpty()){
            //khong null cung khong duoc rong

            //phan biet chu hoa va chu thuong
            constraint = constraint.toString().uppercase()
            val filteredModels = ArrayList<ModelPdf>()
            for(i in filterList.indices){
                //Kiem Tra co khop
                if(filterList[i].title.uppercase().contains(constraint)){
                    //kiem tra trung voi tieu de de them vao list
                    filteredModels.add(filterList[i])
                }
            }
            //tra ve cai danh sach filtered vaf kich thuoc
            results.count = filteredModels.size
            results.values = filteredModels
        }
        else
        {
            //Ke ca null hoac rong
            //tra ve danh sach cu va kich thuoc cu
            results.count = filterList.size
            results.values = filterList
        }

        return results
    }

    override fun publishResults(constraint: CharSequence, results: FilterResults?) {
        //thay doi cac filter
        if (results != null) {
            adapterPdfUser.pdfArrayList = results.values as ArrayList<ModelPdf>
        } /* = java.util.ArrayList<com.example.apptruyen.models.ModelPdf> */

        //thong bao thay doi
        adapterPdfUser.notifyDataSetChanged()
    }
}