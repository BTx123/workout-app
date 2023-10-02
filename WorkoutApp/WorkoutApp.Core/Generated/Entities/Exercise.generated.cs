//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//     Produced by Entity Framework Visual Editor v4.2.5.1
//     Source:                    https://github.com/msawczyn/EFDesigner
//     Visual Studio Marketplace: https://marketplace.visualstudio.com/items?itemName=michaelsawczyn.EFDesigner
//     Documentation:             https://msawczyn.github.io/EFDesigner/
//     License (MIT):             https://github.com/msawczyn/EFDesigner/blob/master/LICENSE
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WorkoutApp.Core.Database
{
   public partial class Exercise
   {
      partial void Init();

      /// <summary>
      /// Default constructor. Protected due to required properties, but present because EF needs it.
      /// </summary>
      protected Exercise()
      {
         _description = "string.Empty";

         Init();
      }

      /// <summary>
      /// Replaces default constructor, since it's protected. Caller assumes responsibility for setting all required values before saving.
      /// </summary>
      public static Exercise CreateExerciseUnsafe()
      {
         return new Exercise();
      }

      /// <summary>
      /// Public constructor with required data
      /// </summary>
      /// <param name="name"></param>
      /// <param name="description"></param>
      public Exercise(string name, string description = "string.Empty")
      {
         if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
         this._name = name;

         if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
         this._description = description;

         Init();
      }

      /// <summary>
      /// Static create function (for use in LINQ queries, etc.)
      /// </summary>
      /// <param name="name"></param>
      /// <param name="description"></param>
      public static Exercise Create(string name, string description = "string.Empty")
      {
         return new Exercise(name, description);
      }

      /*************************************************************************
       * Properties
       *************************************************************************/

      /// <summary>
      /// Backing field for Description
      /// </summary>
      protected string _description;
      /// <summary>
      /// When provided in a partial class, allows value of Description to be changed before setting.
      /// </summary>
      partial void SetDescription(string oldValue, ref string newValue);
      /// <summary>
      /// When provided in a partial class, allows value of Description to be changed before returning.
      /// </summary>
      partial void GetDescription(ref string result);

      /// <summary>
      /// Required, Default value = &quot;string.Empty&quot;
      /// </summary>
      [Required]
      public string Description
      {
         get
         {
            string value = _description;
            GetDescription(ref value);
            return (_description = value);
         }
         set
         {
            string oldValue = Description;
            SetDescription(oldValue, ref value);
            if (oldValue != value)
            {
               _description = value;
               OnPropertyChanged();
            }
         }
      }

      /// <summary>
      /// Identity, Indexed, Required
      /// Unique identifier
      /// </summary>
      [Key]
      [Required]
      [System.ComponentModel.Description("Unique identifier")]
      public long Id { get; set; }

      /// <summary>
      /// Backing field for Name
      /// </summary>
      protected string _name;
      /// <summary>
      /// When provided in a partial class, allows value of Name to be changed before setting.
      /// </summary>
      partial void SetName(string oldValue, ref string newValue);
      /// <summary>
      /// When provided in a partial class, allows value of Name to be changed before returning.
      /// </summary>
      partial void GetName(ref string result);

      /// <summary>
      /// Required
      /// </summary>
      [Required]
      public string Name
      {
         get
         {
            string value = _name;
            GetName(ref value);
            return (_name = value);
         }
         set
         {
            string oldValue = Name;
            SetName(oldValue, ref value);
            if (oldValue != value)
            {
               _name = value;
               OnPropertyChanged();
            }
         }
      }

      /*************************************************************************
       * Navigation properties
       *************************************************************************/

      /// <summary>
      /// Required
      /// </summary>
      public virtual global::WorkoutApp.Core.Database.Barbell Barbell { get; set; }

      /// <summary>
      /// Required
      /// </summary>
      public virtual global::WorkoutApp.Core.Database.SetGroup SetGroup { get; set; }

   }
}

