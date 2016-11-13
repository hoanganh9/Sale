namespace DAL.Entities.Models
{
    using System;
    using System.Collections.Generic;
    
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Base.Common;
    using Base.Lib;
    using Repository.Pattern.Infrastructure;
    using Newtonsoft.Json;
    
    // created : 13/11/2016
    // Author : Generate by Anhhn
    public partial class SI_CategoryDetailModel : EntityModel<SI_CategoryDetail>
    {
    	public SI_CategoryDetailModel()
        {
    		_entity = new SI_CategoryDetail();
    	}
    
    	public SI_CategoryDetailModel(SI_CategoryDetail entity) : base(entity)
        {
        }
    	
    	//TODO: SI_CategoryDetail-Model
    	
    	[Display(Name = "Id")]
    	public byte Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "CategoryId")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.Interger, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(0, 255, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public byte CategoryId
    	{
    		get{ return _entity.CategoryId; }
    		set{ _entity.CategoryId = value; }
    	}
    
    		
    	[Display(Name = "Code")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(20, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Code
    	{
    		get{ return _entity.Code; }
    		set{ _entity.Code = value; }
    	}
    
    		
    	[Display(Name = "Name")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(50, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Name
    	{
    		get{ return _entity.Name; }
    		set{ _entity.Name = value; }
    	}
    
    		
    	[Display(Name = "Description")]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Description
    	{
    		get{ return _entity.Description; }
    		set{ _entity.Description = value; }
    	}
    
    	
        public virtual SI_Category SI_Category
    	{
    		get{ return _entity.SI_Category; }
    		set{ _entity.SI_Category = value; }
    	}
    
    	#region base
    
    	public SI_CategoryDetail toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SI_CategoryDetail entityOld)
    	{
    		entityOld.CategoryId = _entity.CategoryId;
    		entityOld.Code = _entity.Code;
    		entityOld.Name = _entity.Name;
    		entityOld.Description = _entity.Description;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SI_CategoryDetailModelSearch : ModelSearch
    {
        public SI_CategoryDetailModelSearch() : base()
        {
        }
    }
}