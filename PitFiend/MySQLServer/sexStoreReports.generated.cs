#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace MySQLServer	
{
	public partial class sexStoreReports
	{
		private int _id;
		public virtual int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		
		private int _productCode;
		public virtual int ProductCode
		{
			get
			{
				return this._productCode;
			}
			set
			{
				this._productCode = value;
			}
		}
		
		private string _name;
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		
		private string _soldInShops;
		public virtual string SoldInShops
		{
			get
			{
				return this._soldInShops;
			}
			set
			{
				this._soldInShops = value;
			}
		}
		
		private int _totalQuantitySold;
		public virtual int TotalQuantitySold
		{
			get
			{
				return this._totalQuantitySold;
			}
			set
			{
				this._totalQuantitySold = value;
			}
		}
		
		private double _totalIncomes;
		public virtual double TotalIncomes
		{
			get
			{
				return this._totalIncomes;
			}
			set
			{
				this._totalIncomes = value;
			}
		}
		
	}
}
#pragma warning restore 1591
