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
    public partial class AspNetUserModel : EntityModel<AspNetUser>
    {
    	public AspNetUserModel()
        {
    		_entity = new AspNetUser();
    	}
    
    	public AspNetUserModel(AspNetUser entity) : base(entity)
        {
        }
    	
    	//TODO: AspNetUser-Model
    	
    	[Display(Name = "Id")]
    	public string Id
    	{
    		get{ return _entity.Id; }
    		set{ _entity.Id = value; }
    	}
    
    		
    	[Display(Name = "Email")]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string Email
    	{
    		get{ return _entity.Email; }
    		set{ _entity.Email = value; }
    	}
    
    		
    	[Display(Name = "EmailConfirmed")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public bool EmailConfirmed
    	{
    		get{ return _entity.EmailConfirmed; }
    		set{ _entity.EmailConfirmed = value; }
    	}
    
    		
    	[Display(Name = "PasswordHash")]
    	public string PasswordHash
    	{
    		get{ return _entity.PasswordHash; }
    		set{ _entity.PasswordHash = value; }
    	}
    
    		
    	[Display(Name = "SecurityStamp")]
    	public string SecurityStamp
    	{
    		get{ return _entity.SecurityStamp; }
    		set{ _entity.SecurityStamp = value; }
    	}
    
    		
    	[Display(Name = "PhoneNumber")]
    	public string PhoneNumber
    	{
    		get{ return _entity.PhoneNumber; }
    		set{ _entity.PhoneNumber = value; }
    	}
    
    		
    	[Display(Name = "PhoneNumberConfirmed")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public bool PhoneNumberConfirmed
    	{
    		get{ return _entity.PhoneNumberConfirmed; }
    		set{ _entity.PhoneNumberConfirmed = value; }
    	}
    
    		
    	[Display(Name = "TwoFactorEnabled")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public bool TwoFactorEnabled
    	{
    		get{ return _entity.TwoFactorEnabled; }
    		set{ _entity.TwoFactorEnabled = value; }
    	}
    
    		
    	[Display(Name = "LockoutEndDateUtc")]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	public Nullable<System.DateTime> LockoutEndDateUtc
    	{
    		get{ return _entity.LockoutEndDateUtc; }
    		set{ _entity.LockoutEndDateUtc = value; }
    	}
    	[Display(Name = "LockoutEndDateUtc")]
    	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = Enums.FormatModel.FormatDateVN)]
    	[DataType(DataType.Date)]
    	public string _LockoutEndDateUtc
    	{
    		set { _entity.LockoutEndDateUtc = string.IsNullOrWhiteSpace(value)? null : Validate.ConvertDateVNAlowNull(value); }
    		get { return _entity.LockoutEndDateUtc.HasValue? _entity.LockoutEndDateUtc.Value.ToString(Enums.FormatType.FormatDateVN) : string.Empty; }
    	}
    
    		
    	[Display(Name = "LockoutEnabled")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	public bool LockoutEnabled
    	{
    		get{ return _entity.LockoutEnabled; }
    		set{ _entity.LockoutEnabled = value; }
    	}
    
    		
    	[Display(Name = "AccessFailedCount")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[RegularExpression(Enums.RegexDefine.IntergerAm, ErrorMessage = Enums.RegexMessage.Interger)]
    	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = Enums.FormatModel.Integer)]
    	[DataType(DataType.Text)]
    	[Range(-2147483648, 2147483647, ErrorMessage = Enums.ErrorMessage.RangeMinMax)]
    	public int AccessFailedCount
    	{
    		get{ return _entity.AccessFailedCount; }
    		set{ _entity.AccessFailedCount = value; }
    	}
    
    		
    	[Display(Name = "UserName")]
    	[Required(ErrorMessage = Enums.ErrorMessage.Required)]
    	[StringLength(256, ErrorMessage = Enums.ErrorMessage.StringLengthMax)]
    	public string UserName
    	{
    		get{ return _entity.UserName; }
    		set{ _entity.UserName = value; }
    	}
    
    	
    	[JsonIgnore]
        public virtual List<AspNetUserClaim> AspNetUserClaims
    	{
    		get{ return _entity.AspNetUserClaims != null?_entity.AspNetUserClaims.ToList() : new List<AspNetUserClaim>(); }
    		set{ _entity.AspNetUserClaims = value; }
    	}
    	[JsonIgnore]
        public virtual List<AspNetUserLogin> AspNetUserLogins
    	{
    		get{ return _entity.AspNetUserLogins != null?_entity.AspNetUserLogins.ToList() : new List<AspNetUserLogin>(); }
    		set{ _entity.AspNetUserLogins = value; }
    	}
    	[JsonIgnore]
        public virtual List<AspNetRole> AspNetRoles
    	{
    		get{ return _entity.AspNetRoles != null?_entity.AspNetRoles.ToList() : new List<AspNetRole>(); }
    		set{ _entity.AspNetRoles = value; }
    	}
    
    	#region base
    
    	public AspNetUser toCreate()
    	{
    		//if(string.IsNullOrEmpty(_entity.Id))
    		//	_entity.Id = Guid.NewGuid().ToString();
    		
    		//_entity.Status = (int)Enums.StatusBase.Active;
    		//_entity.CreateDate = this.DateChanged;
    		//_entity.CreateBy = this.UserId;
    		return _entity;
    	}
    
    	public void changeEdit(AspNetUser entityOld)
    	{
    		entityOld.Email = _entity.Email;
    		entityOld.EmailConfirmed = _entity.EmailConfirmed;
    		entityOld.PasswordHash = _entity.PasswordHash;
    		entityOld.SecurityStamp = _entity.SecurityStamp;
    		entityOld.PhoneNumber = _entity.PhoneNumber;
    		entityOld.PhoneNumberConfirmed = _entity.PhoneNumberConfirmed;
    		entityOld.TwoFactorEnabled = _entity.TwoFactorEnabled;
    		entityOld.LockoutEndDateUtc = _entity.LockoutEndDateUtc;
    		entityOld.LockoutEnabled = _entity.LockoutEnabled;
    		entityOld.AccessFailedCount = _entity.AccessFailedCount;
    		entityOld.UserName = _entity.UserName;
    		
    		//entityOld.ModifyDate = this.DateChanged;
    		//entityOld.ModifyBy = this.UserId;
    	}
    	#endregion base
    }
    public partial class AspNetUserModelSearch : ModelSearch
    {
        public AspNetUserModelSearch() : base()
        {
        }
    }
}