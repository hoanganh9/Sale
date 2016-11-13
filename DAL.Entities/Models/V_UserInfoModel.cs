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
    public partial class V_UserInfoModel : EntityModel<V_UserInfo>
    {
    	public V_UserInfoModel()
        {
    		_entity = new V_UserInfo();
    	}
    
    	public V_UserInfoModel(V_UserInfo entity) : base(entity)
        {
        }
    	
    	//TODO: V_UserInfo-Model
    	
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
    
    		
    	[Display(Name = "Status")]
    	public int Status
    	{
    		get{ return _entity.Status; }
    		set{ _entity.Status = value; }
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
    
    		
    	[Display(Name = "Email")]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Email
    	{
    		get{ return _entity.Email; }
    		set{ _entity.Email = value; }
    	}
    
    		
    	[Display(Name = "PhoneNumber")]
    	public string PhoneNumber
    	{
    		get{ return _entity.PhoneNumber; }
    		set{ _entity.PhoneNumber = value; }
    	}
    
    		
    	[Display(Name = "UserName")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string UserName
    	{
    		get{ return _entity.UserName; }
    		set{ _entity.UserName = value; }
    	}
    
    	
    	#region base
    
    	public V_UserInfo toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(V_UserInfo entityOld)
    	{
    		entityOld.DislayName = _entity.DislayName;
    		entityOld.Level = _entity.Level;
    		entityOld.Email = _entity.Email;
    		entityOld.PhoneNumber = _entity.PhoneNumber;
    		entityOld.UserName = _entity.UserName;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class V_UserInfoModelSearch : ModelSearch
    {
        public V_UserInfoModelSearch() : base()
        {
        }
    }
}