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
    public partial class SI_CategoryModel : EntityModel<SI_Category>
    {
    	public SI_CategoryModel()
        {
    		_entity = new SI_Category();
    	}
    
    	public SI_CategoryModel(SI_Category entity) : base(entity)
        {
        }
    	
    	//TODO: SI_Category-Model
    	
    	[Display(Name = "Id")]
    	public byte Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "Code")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(10, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
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
    
    	
    	[JsonIgnore]
        public virtual List<SI_CategoryDetail> SI_CategoryDetail
    	{
    		get{ return _entity.SI_CategoryDetail != null?_entity.SI_CategoryDetail.ToList() : new List<SI_CategoryDetail>(); }
    		set{ _entity.SI_CategoryDetail = value; }
    	}
    
    	#region base
    
    	public SI_Category toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(SI_Category entityOld)
    	{
    		entityOld.Code = _entity.Code;
    		entityOld.Name = _entity.Name;
    		entityOld.Description = _entity.Description;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class SI_CategoryModelSearch : ModelSearch
    {
        public SI_CategoryModelSearch() : base()
        {
        }
    }
}