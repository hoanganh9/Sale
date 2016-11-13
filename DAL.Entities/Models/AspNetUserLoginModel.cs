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
    public partial class AspNetUserLoginModel : EntityModel<AspNetUserLogin>
    {
    	public AspNetUserLoginModel()
        {
    		_entity = new AspNetUserLogin();
    	}
    
    	public AspNetUserLoginModel(AspNetUserLogin entity) : base(entity)
        {
        }
    	
    	//TODO: AspNetUserLogin-Model
    	
    	[Display(Name = "LoginProvider")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string LoginProvider
    	{
    		get{ return _entity.LoginProvider; }
    		set{ _entity.LoginProvider = value; }
    	}
    
    		
    	[Display(Name = "ProviderKey")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string ProviderKey
    	{
    		get{ return _entity.ProviderKey; }
    		set{ _entity.ProviderKey = value; }
    	}
    
    		
    	[Display(Name = "UserId")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string UserId
    	{
    		get{ return _entity.UserId; }
    		set{ _entity.UserId = value; }
    	}
    
    	
        public virtual AspNetUser AspNetUser
    	{
    		get{ return _entity.AspNetUser; }
    		set{ _entity.AspNetUser = value; }
    	}
    
    	#region base
    
    	public AspNetUserLogin toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(AspNetUserLogin entityOld)
    	{
    		entityOld.LoginProvider = _entity.LoginProvider;
    		entityOld.ProviderKey = _entity.ProviderKey;
    		entityOld.UserId = _entity.UserId;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class AspNetUserLoginModelSearch : ModelSearch
    {
        public AspNetUserLoginModelSearch() : base()
        {
        }
    }
}