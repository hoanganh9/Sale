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
    public partial class AspNetUserInfoModel : EntityModel<AspNetUserInfo>
    {
    	public AspNetUserInfoModel()
        {
    		_entity = new AspNetUserInfo();
    	}
    
    	public AspNetUserInfoModel(AspNetUserInfo entity) : base(entity)
        {
        }
    	
    	//TODO: AspNetUserInfo-Model
    	
    	[Display(Name = "Id")]
    	public string Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "DislayName")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string DislayName
    	{
    		get{ return _entity.DislayName; }
    		set{ _entity.DislayName = value; }
    	}
    
    		
    	[Display(Name = "Level")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public int Level
    	{
    		get{ return _entity.Level; }
    		set{ _entity.Level = value; }
    	}
    
    		
    	[Display(Name = "Status")]
    	public int Status
    	{
    		get{ return _entity.Status; }
    		set{ _entity.Status = value; }
    	}
    
    		
    	[Display(Name = "CreateDate")]
    	public System.DateTime CreateDate
    	{
    		get{ return _entity.CreateDate; }
    		set{ _entity.CreateDate = value; }
    	}
    
    		
    	[Display(Name = "CreateBy")]
    	public string CreateBy
    	{
    		get{ return _entity.CreateBy; }
    		set{ _entity.CreateBy = value; }
    	}
    
    		
    	[Display(Name = "ModifyDate")]
    	public Nullable<System.DateTime> ModifyDate
    	{
    		get{ return _entity.ModifyDate; }
    		set{ _entity.ModifyDate = value; }
    	}
    
    		
    	[Display(Name = "ModifyBy")]
    	public string ModifyBy
    	{
    		get{ return _entity.ModifyBy; }
    		set{ _entity.ModifyBy = value; }
    	}
    
    	
    	#region base
    
    	public AspNetUserInfo toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(AspNetUserInfo entityOld)
    	{
    		entityOld.DislayName = _entity.DislayName;
    		entityOld.Level = _entity.Level;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class AspNetUserInfoModelSearch : ModelSearch
    {
        public AspNetUserInfoModelSearch() : base()
        {
        }
    }
}