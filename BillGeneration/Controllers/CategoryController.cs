using BillGeneration.BusinessLayer;
using BillGeneration.DataContract;
using System;
using System.Collections.Generic;
using System.Web.Http;
namespace BillGeneration.Controllers
{
    //[RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        [AllowAnonymous]

        #region Category Master

        #region Add Update Category
        [HttpPost]
        public long AddUpdateCategoryMaster(CategoryMasterDc CategoryMasterDc)
        {
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryMasterDc.CategoryId = CategoryManager.InsertUpdate_Category(CategoryMasterDc);
            }
            catch (Exception ex)
            {
            }
            return CategoryMasterDc.CategoryId;
        }
        #endregion

        #region Get 

        #region Get Category By Id
        [HttpGet]
        public CategoryMasterDc GetCategoryById(int CategoryId)
        {
            CategoryMasterDc CategoryMasterDc = null;
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryMasterDc = CategoryManager.GetCategoryById(CategoryId);
            }
            catch (Exception ex)
            {
            }
            return CategoryMasterDc;
        }
        #endregion

        #region Search Category
        [HttpPost]
        public List<CategoryMasterDc> SearchCategory(CategorySearchDc CategorySearchDc)
        {
            List<CategoryMasterDc> CategoryMasterDc = null;
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryMasterDc = CategoryManager.SearchCategory(CategorySearchDc);
            }
            catch (Exception ex)
            {
            }
            return CategoryMasterDc;
        }
        #endregion

        #endregion

        #region Delete Category By Id 
        [HttpGet]
        public bool DeleteCategoryById(int CategoryId)
        {
            bool Ans = false;
            try
            {
                CategoryManager CategoryManager = new CategoryManager();
                Ans = CategoryManager.DeleteCategory(CategoryId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion

        #region Category Reciept

        #region Add Update Category Reciept
        [HttpPost]
        public long AddUpdateCategoryReciept(CategoryRecieptDc CategoryRecieptDc)
        {
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryRecieptDc.CategoryRecieptId = CategoryManager.InsertUpdate_CategoryReciept(CategoryRecieptDc);
            }
            catch (Exception ex)
            {
            }
            return CategoryRecieptDc.CategoryRecieptId;
        }
        #endregion

        #region Get 

        #region Get Category Reciept By Id
        [HttpGet]
        public CategoryRecieptDc GetCategoryRecieptById(int CategoryRecieptId)
        {
            CategoryRecieptDc CategoryRecieptDc = null;
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryRecieptDc = CategoryManager.GetCategoryRecieptById(CategoryRecieptId);
            }
            catch (Exception ex)
            {
            }
            return CategoryRecieptDc;
        }
        #endregion

        #region Get Category Reciept
        [HttpGet]
        public List<CategoryRecieptDc> GetCategoryReciept(int CategoryId)
        {
            List<CategoryRecieptDc> CategoryRecieptDc = null;
            CategoryManager CategoryManager = new CategoryManager();
            try
            {
                CategoryRecieptDc = CategoryManager.SearchCategoryReciept(CategoryId);
            }
            catch (Exception ex)
            {
            }
            return CategoryRecieptDc;
        }
        #endregion

        #endregion

        #region Delete Category Reciept By Id 
        [HttpGet]
        public bool DeleteCategoryRecieptById(int CategoryRecieptId)
        {
            bool Ans = false;
            try
            {
                CategoryManager CategoryManager = new CategoryManager();
                Ans = CategoryManager.DeleteCategoryReciept(CategoryRecieptId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion 

    }
}
