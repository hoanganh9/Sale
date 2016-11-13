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
    public partial class AspNetUserClaimModel : EntityModel<AspNetUserClaim>
    {
    	public AspNetUserClaimModel()
        {
    		_entity = new AspNetUserClaim();
    	}
    
    	public AspNetUserClaimModel(AspNetUserClaim entity) : base(entity)
        {
        }
    	
    	//TODO: AspNetUserClaim-Model
    	
    	[Display(Name = "Id")]
    	public int Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "UserId")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(128, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string UserId
    	{
    		get{ return _entity.UserId; }
    		set{ _entity.UserId = value; }
    	}
    
    		
    	[Display(Name = "ClaimType")]
    	public string ClaimType
    	{
    		get{ return _entity.ClaimType; }
    		set{ _entity.ClaimType = value; }
    	}
    
    		
    	[Display(Name = "ClaimValue")]
    	public string ClaimValue
    	{
    		get{ return _entity.ClaimValue; }
    		set{ _entity.ClaimValue = value; }
    	}
    
    	
        public virtual AspNetUser AspNetUser
    	{
    		get{ return _entity.AspNetUser; }
    		set{ _entity.AspNetUser = value; }
    	}
    
    	#region base
    
    	public AspNetUserClaim toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(AspNetUserClaim entityOld)
    	{
    		entityOld.UserId = _entity.UserId;
    		entityOld.ClaimType = _entity.ClaimType;
    		entityOld.ClaimValue = _entity.ClaimValue;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class AspNetUserClaimModelSearch : ModelSearch
    {
        public AspNetUserClaimModelSearch() : base()
        {
        }
    }
}