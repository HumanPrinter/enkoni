//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the generic functionality of a repository.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.Practices.EnterpriseLibrary.Validation;

using OscarBrouwer.Framework.Validation;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This abstract class defines the API of a repository that is capable of accessing specific types in a 
  /// persistency.</summary>
  /// <typeparam name="T">The type of entity that is managed by the repository.</typeparam>
  public abstract class Repository<T> where T : class {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Repository{T}"/> class.</summary>
    protected Repository() {
      /* Set a default validator */
      this.Validator = new EntityValidator<T>();
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the instance that must be used to validate the entities before adding or updating them in
    /// the repository.</summary>
    public EntityValidator<T> Validator { get; set; }

    /// <summary>Gets the wildcard that is used to match a single character.</summary>
    protected virtual string SinglePositionWildcard {
      get { return "?"; }
    }

    /// <summary>Gets the wildcard that is used to match zero or more character.</summary>
    protected virtual string MultiplePositionWildcard {
      get { return "*"; }
    }
    #endregion

    #region CRUD methods
    /// <summary>Saves all the changes to the underlying persistency.</summary>
    public void SaveChanges() {
      this.SaveChangesCore();
    }

    /// <summary>Creates a new entity. To add the entity to the repository, use the <see cref="M:AddEntity(T)"/> 
    /// method with the returned value.</summary>
    /// <returns>The newly created entity.</returns>
    public T CreateEntity() {
      return this.CreateEntityCore();
    }

    /// <summary>Adds a new entity to the repository. Call <see cref="M:SaveChanges()"/> to make the addition
    /// permanent.</summary>
    /// <param name="entity">The entity that must be added to the repository.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T AddEntity(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }
      
      if(this.Validator != null) {
        ValidationResults results = this.Validator.PerformDeepValidation(entity);
        if(!results.IsValid) {
          throw new ValidationException("The entity is not valid and will not be added to the repository", results);
        }
      }

      return this.AddEntityCore(entity);
    }

    /// <summary>Updates the repository with the changes made to the entity. Call <see cref="M:SaveChanges()"/> to make
    /// the changes permanent.</summary>
    /// <param name="entity">The entity whose members are updated.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T UpdateEntity(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      if(this.Validator != null) {
        ValidationResults results = this.Validator.PerformDeepValidation(entity);
        if(!results.IsValid) {
          throw new ValidationException("The entity is not valid and will not be added to the repository", results);
        }
      }

      return this.UpdateEntityCore(entity);
    }

    /// <summary>Deletes the entity from the repository. Call <see cref="M:SaveChanges()"/> to make the changes 
    /// permanent.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    public void DeleteEntity(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      this.DeleteEntityCore(entity);
    }
    #endregion

    #region Select methods
    /// <summary>Finds all the entities of type <typeparamref name="T"/>.</summary>
    /// <returns>All the available entities.</returns>
    public IEnumerable<T> FindAll() {
      return this.FindAllCore();
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <returns>The entities that match the specified expression.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="expression"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> FindAll(Func<T, bool> expression) {
      if(expression == null) {
        throw new ArgumentNullException("expression");
      }

      return this.FindAllCore(expression);
    }

    /// <summary>Finds a single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="expression"/> is <see langword="null"/>.</exception>
    public T FindSingle(Func<T, bool> expression) {
      if(expression == null) {
        throw new ArgumentNullException("expression");
      }

      return this.FindSingleCore(expression);
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="expression"/> is <see langword="null"/>.</exception>
    public T FindSingle(Func<T, bool> expression, T defaultValue) {
      if(expression == null) {
        throw new ArgumentNullException("expression");
      }

      return this.FindSingleCore(expression, defaultValue);
    }

    /// <summary>Finds the first entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="expression"/> is <see langword="null"/>.</exception>
    public T FindFirst(Func<T, bool> expression) {
      if(expression == null) {
        throw new ArgumentNullException("expression");
      }

      return this.FindFirstCore(expression);
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="expression"/> is <see langword="null"/>.</exception>
    public T FindFirst(Func<T, bool> expression, T defaultValue) {
      if(expression == null) {
        throw new ArgumentNullException("expression");
      }

      return this.FindFirstCore(expression, defaultValue);
    }
    #endregion

    #region Create Expression methods
    /// <summary>Creates an expression that executes a 'Like' comparison. This type of comparison checks if a 
    /// text-field matches a given pattern. The returned expression uses the field that must be matched as the first 
    /// input parameter and the pattern as the second input parameter.</summary>
    /// <example>
    /// <![CDATA[
    /// IEnumerable<MyType> source;
    /// Func<bool, string, string> expr = CreateLikeExpression();
    /// IEnumerable<MyType> result = source.Where(s=>expression(s.SomeStringField, "a?"));
    /// ]]>
    /// </example>
    /// <returns>The created expression.</returns>
    public Func<string, string, bool> CreateLikeExpression() {
      return this.CreateLikeExpressionCore();
    }
    #endregion

    #region Extensibility methods
    /// <summary>Saves all the changes to the underlying persistency.</summary>
    protected abstract void SaveChangesCore();

    /// <summary>Creates a new entity.</summary>
    /// <returns>The newly created entity.</returns>
    protected abstract T CreateEntityCore();

    /// <summary>Adds a new entity to the repository.</summary>
    /// <param name="entity">The entity that must be added to the repository.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T AddEntityCore(T entity);

    /// <summary>Updates the repository with the changes made to the entity.</summary>
    /// <param name="entity">The entity whose members are updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T UpdateEntityCore(T entity);

    /// <summary>Deletes the entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    protected abstract void DeleteEntityCore(T entity);

    /// <summary>Finds all the entities of type <typeparamref name="T"/>.</summary>
    /// <returns>All the available entities.</returns>
    protected abstract IEnumerable<T> FindAllCore();

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected abstract IEnumerable<T> FindAllCore(Func<T, bool> expression);

    /// <summary>Finds a single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    protected abstract T FindSingleCore(Func<T, bool> expression);

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected abstract T FindSingleCore(Func<T, bool> expression, T defaultValue);

    /// <summary>Finds the first single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    protected abstract T FindFirstCore(Func<T, bool> expression);

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected abstract T FindFirstCore(Func<T, bool> expression, T defaultValue);

    /// <summary>Creates an expression that executes a 'Like' comparison.</summary>
    /// <returns>The created expression.</returns>
    protected abstract Func<string, string, bool> CreateLikeExpressionCore();
    #endregion
  }
}
