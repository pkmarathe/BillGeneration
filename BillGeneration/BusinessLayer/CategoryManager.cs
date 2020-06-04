using BillGeneration.DataContract;
using BillGeneration.DataLayer;
using BillGeneration.DataLayer.EF;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillGeneration.BusinessLayer
{
    public class CategoryManager
    {
        Repository objRep = new Repository();

        #region Category

        #region Add Update Category
        public int InsertUpdate_Category(CategoryMasterDc Obj)
        {
            CategoryMaster CategoryMasters = new CategoryMaster();
            if (Obj.CategoryId > 0)
            {
                CategoryMasters = objRep.Get<CategoryMaster>(Obj.CategoryId);
                CategoryMasters.CategoryName = Obj.CategoryName;
                CategoryMasters.IsActive = Obj.IsActive;
                objRep.Update<CategoryMaster>(CategoryMasters);
            }
            else
            {
                CategoryMasters.CategoryId = 0;
                CategoryMasters.CategoryName = Obj.CategoryName;
                CategoryMasters.IsActive = Obj.IsActive;
                CategoryMasters.CreatedDate = DateTime.Now;
                objRep.Add<CategoryMaster>(CategoryMasters);
            }
            return CategoryMasters.CategoryId;
        }

        #endregion 

        #region DeleteCategory
        public bool DeleteCategory(int CategoryId)
        {
            bool Ans = false;
            try
            {
                CategoryMaster CategoryMaster = new CategoryMaster();
                if (CategoryId > 0)
                {
                    CategoryMaster = objRep.Get<CategoryMaster>(CategoryId);
                    CategoryMaster.IsActive = false;
                    Ans = objRep.Update<CategoryMaster>(CategoryMaster);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Category By Id
        public CategoryMasterDc GetCategoryById(int CategoryId)
        {
            try
            {
                CategoryMasterDc CategoryMasterDc = objRep.FindAllBy<CategoryMaster>(x => x.CategoryId == CategoryId).Select(x => new CategoryMasterDc
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).FirstOrDefault();

                return CategoryMasterDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search Category
        public List<CategoryMasterDc> SearchCategory(CategorySearchDc CategorySearchDc)
        {
            var LoginPredicate = PredicateBuilder.New<CategoryMaster>(true);

            List<CategoryMasterDc> CategoryMasterDcList;

            if (!string.IsNullOrEmpty(CategorySearchDc.CategoryName))
            {
                LoginPredicate = LoginPredicate.And(p => p.CategoryName.Contains(CategorySearchDc.CategoryName));
            }
            if (CategorySearchDc.IsActive.ToString() != "All" && !string.IsNullOrEmpty(CategorySearchDc.IsActive.ToString()))
            {
                LoginPredicate = LoginPredicate.And(p => p.IsActive.ToString() == CategorySearchDc.IsActive);
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.CategoryId.ToString()) && CategorySearchDc.CategoryId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.CategoryId.ToString() == CategorySearchDc.CategoryId.ToString());
            }
            CategoryMasterDcList = objRep.FindAllBy1<CategoryMaster>(LoginPredicate, x => x.OrderBy(y => y.CategoryId)).Select(x => new CategoryMasterDc
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate,
            }).ToList();

            return CategoryMasterDcList;
        }
        #endregion

        #endregion

        #region Category Reciepts

        #region Add Update Category Reciepts
        public int InsertUpdate_CategoryReciept(CategoryRecieptDc Obj)
        {
            CategoryRecieptMaster CategoryMasters = new CategoryRecieptMaster();
            if (Obj.CategoryRecieptId > 0)
            {
                CategoryMasters = objRep.Get<CategoryRecieptMaster>(Obj.CategoryRecieptId);
                CategoryMasters.CategoryId = Obj.CategoryId;
                CategoryMasters.BillRecieptName = Obj.BillRecieptName;
                CategoryMasters.BillRecieptDynamicHtml = Obj.BillRecieptDynamicHtml;
                CategoryMasters.BillRecieptSampleImage = Obj.BillRecieptSampleImage;
                CategoryMasters.RecieptType = Obj.RecieptType;
                CategoryMasters.BillRecieptPdfHtml = Obj.BillRecieptPdfHtml;
                CategoryMasters.IsRecieptLogo = Obj.IsRecieptLogo;
                CategoryMasters.ReceiptWidth = Obj.ReceiptWidth;
                CategoryMasters.ReceiptHight = Obj.ReceiptHight; 
                CategoryMasters.FontId = Obj.FontId; 
                objRep.Update<CategoryRecieptMaster>(CategoryMasters);
            }
            else
            {
                CategoryMasters.CategoryRecieptId = 0;
                CategoryMasters.CategoryId = Obj.CategoryId;
                CategoryMasters.BillRecieptDynamicHtml = Obj.BillRecieptDynamicHtml;
                CategoryMasters.BillRecieptName = Obj.BillRecieptName;
                CategoryMasters.IsActive = Obj.IsActive;
                CategoryMasters.BillRecieptSampleImage = Obj.BillRecieptSampleImage;
                CategoryMasters.RecieptType = Obj.RecieptType;
                CategoryMasters.BillRecieptPdfHtml = Obj.BillRecieptPdfHtml;
                CategoryMasters.IsRecieptLogo = Obj.IsRecieptLogo;
                CategoryMasters.ReceiptWidth = Obj.ReceiptWidth;
                CategoryMasters.ReceiptHight = Obj.ReceiptHight;
                CategoryMasters.FontId = Obj.FontId;
                CategoryMasters.CreatedDate = DateTime.Now;
                objRep.Add<CategoryRecieptMaster>(CategoryMasters);
            }
            return CategoryMasters.CategoryRecieptId;
        }

        #endregion 

        #region Delete Category Reciepts
        public bool DeleteCategoryReciept(int CategoryRecieptId)
        {
            bool Ans = false;
            try
            {
                CategoryRecieptMaster CategoryRecieptMaster = new CategoryRecieptMaster();
                if (CategoryRecieptId > 0)
                {
                    CategoryRecieptMaster = objRep.Get<CategoryRecieptMaster>(CategoryRecieptId);
                    CategoryRecieptMaster.IsActive = false;
                    Ans = objRep.Update<CategoryRecieptMaster>(CategoryRecieptMaster);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Category Reciept By Id
        public CategoryRecieptDc GetCategoryRecieptById(int CategoryRecieptId)
        {
            CategoryRecieptDc CategoryRecieptDc = new CategoryRecieptDc();
            try
            {
                CategoryRecieptDc = objRep.FindAllBy<CategoryRecieptMaster>(x => x.CategoryRecieptId == CategoryRecieptId).Select(x => new CategoryRecieptDc
                {
                    CategoryRecieptId = x.CategoryRecieptId,
                    CategoryId = x.CategoryId,
                    BillRecieptDynamicHtml = x.BillRecieptDynamicHtml,
                    BillRecieptName = x.BillRecieptName,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    BillRecieptSampleImage = x.BillRecieptSampleImage,
                    RecieptType = x.RecieptType,
                    BillRecieptPdfHtml = x.BillRecieptPdfHtml,
                    IsRecieptLogo = x.IsRecieptLogo,
                    ReceiptWidth = x.ReceiptWidth,
                    ReceiptHight = x.ReceiptHight,
                    FontId = x.FontId,
                }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            return CategoryRecieptDc;
        }
        #endregion

        #region Search Category Reciept
        public List<CategoryRecieptDc> SearchCategoryReciept(int CategoryId)
        {
            List<CategoryRecieptDc> CategoryRecieptDc = new List<CategoryRecieptDc>();
            try
            {
                CategoryRecieptDc = objRep.FindAllBy1<CategoryRecieptMaster>(y => y.CategoryId == CategoryId && y.IsActive == true, x => x.OrderBy(y => y.CategoryRecieptId)).Select(x => new CategoryRecieptDc
                {
                    CategoryRecieptId = x.CategoryRecieptId,
                    CategoryId = x.CategoryId,
                    BillRecieptDynamicHtml = x.BillRecieptDynamicHtml,
                    BillRecieptName = x.BillRecieptName,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                    BillRecieptSampleImage = x.BillRecieptSampleImage,
                    RecieptType = x.RecieptType,
                    BillRecieptPdfHtml = x.BillRecieptPdfHtml,
                    IsRecieptLogo = x.IsRecieptLogo,
                    ReceiptWidth = x.ReceiptWidth,
                    ReceiptHight = x.ReceiptHight,
                    FontId = x.FontId,
                }).ToList();
            }
            catch { }

            return CategoryRecieptDc;
        }
        #endregion

        #endregion

    }
}
