using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using Enkoni.Framework.Linq;

using LinqKit;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the API of a repository that is capable of accessing specific types in a persistency.</summary>
  /// <typeparam name="T">The type of entity that is managed by the repository.</typeparam>
  public abstract class Repository<T> : ISpecificationVisitor<T>, IDisposable where T : class {
    #region Instance variables
    /// <summary>Indicates if the type of entity implements the ICloneable interface.</summary>
    private bool typeImplementsICloneable;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Repository{T}"/> class.</summary>
    protected Repository() {
      this.typeImplementsICloneable = typeof(T).GetInterfaces().Contains(typeof(ICloneable));

      /* By default, if the type implements ICloneable, it must be cloned. */
      this.CloneDataSourceItems = this.typeImplementsICloneable;

      /* Set a default validator */
      this.Validator = new EntityValidator<T>();
    }

    /// <summary>Initializes a new instance of the <see cref="Repository{T}"/> class.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the datasource.</param>
    /// <exception cref="InvalidOperationException"><paramref name="dataSourceInfo"/> specifies that data source items must be cloned, but 
    /// <typeparamref name="T"/> does not implement <see cref="ICloneable"/>.</exception>
    protected Repository(DataSourceInfo dataSourceInfo)
      : this() {
      this.CloneDataSourceItems = DataSourceInfo.SelectCloneDataSourceItems(dataSourceInfo);

      if(this.CloneDataSourceItems && !this.typeImplementsICloneable) {
        throw new InvalidOperationException("Cannot clone data source items because the type does not implement ICloneable.");
      }
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the instance that must be used to validate the entities before adding or updating them in the repository.</summary>
    public EntityValidator<T> Validator { get; set; }

    /// <summary>Gets a value indicating whether entities retrieved from the datasource must be cloned or not.</summary>
    public bool CloneDataSourceItems { get; private set; }
    #endregion

    #region ISpecificationVisitor implementation
    /// <summary>Creates an AND-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateAndExpression(ISpecification<T> leftOperand,
      ISpecification<T> rightOperand) {
      return this.CreateAndExpressionCore(leftOperand, rightOperand);
    }

    /// <summary>Creates an OR-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateOrExpression(ISpecification<T> leftOperand,
      ISpecification<T> rightOperand) {
      return this.CreateOrExpressionCore(leftOperand, rightOperand);
    }

    /// <summary>Creates a lambda-expression using the specified expression. Typically, this method simply returns the parameter.</summary>
    /// <param name="expression">The expression that was originally passed to the specification.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateLambdaExpression(Expression<Func<T, bool>> expression) {
      return this.CreateLambdaExpressionCore(expression);
    }

    /// <summary>Creates a NOT-expression using the specified specification.</summary>
    /// <param name="specification">The specification whose result must be inverted.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateNotExpression(ISpecification<T> specification) {
      return this.CreateNotExpressionCore(specification);
    }

    /// <summary>Creates a LIKE-expression using the specified field and searchpattern.</summary>
    /// <param name="field">The field of type <c>T</c> that must match the pattern.</param>
    /// <param name="pattern">The pattern to which the field must apply. The pattern may contain a '*' and '?' wildcard.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateLikeExpression(Expression<Func<T, string>> field,
      string pattern) {
      return this.CreateLikeExpressionCore(field, pattern);
    }

    /// <summary>Creates a lambda-expression using the custom specification. This method is executed when a specification-type is used that is not 
    /// part of the default specification system.</summary>
    /// <param name="specification">The custom specification.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> ISpecificationVisitor<T>.CreateCustomExpression(ISpecification<T> specification) {
      return this.CreateCustomExpressionCore(specification);
    }
    #endregion

    #region CRUD methods
    /// <summary>Saves all the changes to the underlying persistency.</summary>
    public void SaveChanges() {
      this.SaveChanges(null);
    }

    /// <summary>Saves all the changes to the underlying persistency.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    public void SaveChanges(DataSourceInfo dataSourceInfo) {
      this.SaveChangesCore(dataSourceInfo);
    }

    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    public void Reset() {
      this.Reset(null);
    }

    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    public void Reset(DataSourceInfo dataSourceInfo) {
      this.ResetCore(dataSourceInfo);
    }

    /// <summary>Creates a new entity. To add the entity to the repository, use the <see cref="AddEntity(T)"/> method with the returned value.
    /// </summary>
    /// <returns>The newly created entity.</returns>
    public T CreateEntity() {
      return this.CreateEntity(null);
    }

    /// <summary>Creates a new entity. To add the entity to the repository, use the <see cref="AddEntity(T)"/> method with the returned value.
    /// </summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The newly created entity.</returns>
    public T CreateEntity(DataSourceInfo dataSourceInfo) {
      return this.CreateEntityCore(dataSourceInfo);
    }

    /// <summary>Adds a new entity to the repository. Call <see cref="SaveChanges()"/> to make the addition permanent.</summary>
    /// <param name="entity">The entity that must be added to the repository.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">If <paramref name="entity"/> contains invalid values.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T AddEntity(T entity) {
      return this.AddEntity(entity, null);
    }

    /// <summary>Adds a new entity to the repository. Call <see cref="SaveChanges()"/> to make the addition permanent.</summary>
    /// <param name="entity">The entity that must be added to the repository.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">If <paramref name="entity"/> contains invalid values.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T AddEntity(T entity, DataSourceInfo dataSourceInfo) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      if(this.Validator != null) {
        try {
          this.Validator.PerformValidation(entity, true);
        }
        catch(ValidationException ex) {
          throw new ValidationException("The entity is not valid and will not be added to the repository", ex);
        }
      }

      return this.AddEntityCore(entity, dataSourceInfo);
    }

    /// <summary>Adds a collection of new entities to the repository. Call <see cref="SaveChanges()"/> to make the additions permanent.</summary>
    /// <param name="entities">The entities that must be added to the repository.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    /// <exception cref="ValidationException">If one or more of the entities contains invalid values.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public IEnumerable<T> AddEntities(IEnumerable<T> entities) {
      return this.AddEntities(entities, null);
    }

    /// <summary>Adds a collection of new entities to the repository.</summary>
    /// <param name="entities">The entities that must be added to the repository.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    /// <exception cref="ValidationException">If one or more of the entities contains invalid values.</exception>
    /// <returns>The entities with the most recent values.</returns>
    public IEnumerable<T> AddEntities(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      if(entities == null) {
        throw new ArgumentNullException("entities");
      }

      if(entities.Count() == 0) {
        throw new ArgumentException("Cannot add an empty collection to the repository", "entities");
      }

      if(this.Validator != null) {
        try {
          foreach(T entity in entities) {
            this.Validator.PerformValidation(entity, true);
          }
        }
        catch(ValidationException ex) {
          throw new ValidationException("One or more entities are not valid and no entity will be added to the repository", ex);
        }
      }

      return this.AddEntitiesCore(entities, dataSourceInfo);
    }

    /// <summary>Updates the repository with the changes made to the entity. Call <see cref="SaveChanges()"/> to make the changes permanent.
    /// </summary>
    /// <param name="entity">The entity whose members are updated.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">If <paramref name="entity"/> contains invalid values.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T UpdateEntity(T entity) {
      return this.UpdateEntity(entity, null);
    }

    /// <summary>Updates the repository with the changes made to the entity. Call <see cref="SaveChanges()"/> to make the changes permanent.
    /// </summary>
    /// <param name="entity">The entity whose members are updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <exception cref="ValidationException">If <paramref name="entity"/> contains invalid values.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T UpdateEntity(T entity, DataSourceInfo dataSourceInfo) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      if(this.Validator != null) {
        try {
          this.Validator.PerformValidation(entity, true);
        }
        catch(ValidationException ex) {
          throw new ValidationException("The entity is not valid and will not be added to the repository", ex);
        }
      }

      return this.UpdateEntityCore(entity, dataSourceInfo);
    }

    /// <summary>Updates the repository with the changes made to the entities.</summary>
    /// <param name="entities">The entities whose members have changed.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    /// <exception cref="ValidationException">If one or more of the entities contains invalid values.</exception>
    /// <returns>The entities with the most recent values.</returns>
    public IEnumerable<T> UpdateEntities(IEnumerable<T> entities) {
      return this.UpdateEntities(entities, null);
    }

    /// <summary>Updates the repository with the changes made to the entities.</summary>
    /// <param name="entities">The entities whose members have changed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    /// <exception cref="ValidationException">If one or more of the entities contains invalid values.</exception>
    /// <returns>The entities with the most recent values.</returns>
    public IEnumerable<T> UpdateEntities(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      if(entities == null) {
        throw new ArgumentNullException("entities");
      }

      if(entities.Count() == 0) {
        throw new ArgumentException("Cannot update the repository with an empty collection", "entities");
      }

      if(this.Validator != null) {
        try {
          foreach(T entity in entities) {
            this.Validator.PerformValidation(entity, true);
          }
        }
        catch(ValidationException ex) {
          throw new ValidationException("One or more entities are not valid and no entity will not be added to the repository", ex);
        }
      }

      return this.UpdateEntitiesCore(entities, dataSourceInfo);
    }

    /// <summary>Deletes the entity from the repository. Call <see cref="SaveChanges()"/> to make the changes permanent.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    public void DeleteEntity(T entity) {
      this.DeleteEntity(entity, null);
    }

    /// <summary>Deletes the entity from the repository. Call <see cref="SaveChanges()"/> to make the changes permanent.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    public void DeleteEntity(T entity, DataSourceInfo dataSourceInfo) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      this.DeleteEntityCore(entity, dataSourceInfo);
    }

    /// <summary>Deletes the entities from the repository. Call <see cref="SaveChanges()"/> to make the changes permanent.</summary>
    /// <param name="entities">The entities that must be deleted.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    public void DeleteEntities(IEnumerable<T> entities) {
      this.DeleteEntities(entities, null);
    }

    /// <summary>Deletes the entities from the repository. Call <see cref="SaveChanges()"/> to make the changes permanent.</summary>
    /// <param name="entities">The entities that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="entities"/> is empty.</exception>
    public void DeleteEntities(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      if(entities == null) {
        throw new ArgumentNullException("entities");
      }

      if(entities.Count() == 0) {
        throw new ArgumentException("Cannot remove an empty collection from the repository", "entities");
      }

      this.DeleteEntitiesCore(entities, dataSourceInfo);
    }

    /// <summary>Executes an action in the repository as specified by <paramref name="specification"/>.</summary>
    /// <param name="specification">The specification that indicates the action that must be executed.</param>
    /// <returns>An object describing the result of the action.</returns>
    public object Execute(ISpecification<T> specification) {
      return this.Execute(specification, null);
    }

    /// <summary>Executes an action in the repository as specified by <paramref name="specification"/>.</summary>
    /// <param name="specification">The specification that indicates the action that must be executed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>An object describing the result of the action.</returns>
    public object Execute(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      return this.Execute<object>(specification, dataSourceInfo);
    }

    /// <summary>Executes an action in the repository as specified by <paramref name="specification"/>.</summary>
    /// <typeparam name="TResult">The type of object that is returned by the action.</typeparam>
    /// <param name="specification">The specification that indicates the action that must be executed.</param>
    /// <returns>An object describing the result of the action.</returns>
    public TResult Execute<TResult>(ISpecification<T> specification) {
      return this.Execute<TResult>(specification, null);
    }

    /// <summary>Executes an action in the repository as specified by <paramref name="specification"/>.</summary>
    /// <typeparam name="TResult">The type of object that is returned by the action.</typeparam>
    /// <param name="specification">The specification that indicates the action that must be executed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>An object describing the result of the action.</returns>
    public TResult Execute<TResult>(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      return this.ExecuteCore<TResult>(specification, dataSourceInfo);
    }
    #endregion

    #region Select methods
    /// <summary>Finds all the entities of type <typeparamref name="T"/>.</summary>
    /// <returns>All the available entities.</returns>
    public IEnumerable<T> FindAll() {
      return this.FindAll((DataSourceInfo)null);
    }

    /// <summary>Finds all the entities of type <typeparamref name="T"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>All the available entities.</returns>
    public IEnumerable<T> FindAll(DataSourceInfo dataSourceInfo) {
      return this.FindAllCore(dataSourceInfo);
    }

    /// <summary>Finds all the available entities that match the specification.</summary>
    /// <param name="specification">The specification to which entities must match.</param>
    /// <returns>The entities that match the specification.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> FindAll(ISpecification<T> specification) {
      return this.FindAll(specification, null);
    }

    /// <summary>Finds all the available entities that match the specification.</summary>
    /// <param name="specification">The specification to which the entities must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specification.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public IEnumerable<T> FindAll(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      BusinessRuleSpecification<T> businessRule = specification as BusinessRuleSpecification<T>;
      if(businessRule != null) {
        return this.ExecuteBusinessRuleWithMultipleResults(businessRule.RuleName, businessRule.RuleArguments);
      }

      Expression<Func<T, bool>> expression = specification.Visit(this);
      return this.FindAllCore(expression, specification.SortRules, specification.MaximumResults, dataSourceInfo);
    }

    /// <summary>Finds a single entity that matches the specification.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindSingle(ISpecification<T> specification) {
      return this.FindSingle(specification, (DataSourceInfo)null);
    }

    /// <summary>Finds a single entity that matches the specification.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindSingle(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      BusinessRuleSpecification<T> businessRule = specification as BusinessRuleSpecification<T>;
      if(businessRule != null) {
        return this.ExecuteBusinessRuleWithSingleResult(businessRule.RuleName, businessRule.RuleArguments);
      }

      Expression<Func<T, bool>> expression = specification.Visit(this);
      return this.FindSingleCore(expression, dataSourceInfo);
    }

    /// <summary>Finds a single entity that matches the specification. If no result was found, the specified default-value is returned.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindSingle(ISpecification<T> specification, T defaultValue) {
      return this.FindSingle(specification, defaultValue, null);
    }

    /// <summary>Finds a single entity that matches the specification. If no result was found, the specified default-value is returned.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindSingle(ISpecification<T> specification, T defaultValue, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      BusinessRuleSpecification<T> businessRule = specification as BusinessRuleSpecification<T>;
      if(businessRule != null) {
        return this.ExecuteBusinessRuleWithSingleResult(businessRule.RuleName, businessRule.RuleArguments);
      }

      Expression<Func<T, bool>> expression = specification.Visit(this);
      return this.FindSingleCore(expression, dataSourceInfo, defaultValue);
    }

    /// <summary>Finds the first entity that matches the specification.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindFirst(ISpecification<T> specification) {
      return this.FindFirst(specification, (DataSourceInfo)null);
    }

    /// <summary>Finds the first entity that matches the specification.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindFirst(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      BusinessRuleSpecification<T> businessRule = specification as BusinessRuleSpecification<T>;
      if(businessRule != null) {
        return this.ExecuteBusinessRuleWithSingleResult(businessRule.RuleName, businessRule.RuleArguments);
      }

      Expression<Func<T, bool>> expression = specification.Visit(this);
      return this.FindFirstCore(expression, specification.SortRules, dataSourceInfo);
    }

    /// <summary>Finds the first entity that matches the specification. If no result was found, the specified default-value is returned.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindFirst(ISpecification<T> specification, T defaultValue) {
      return this.FindFirst(specification, defaultValue, null);
    }

    /// <summary>Finds the first entity that matches the specification. If no result was found, the specified default-value is returned.</summary>
    /// <param name="specification">The specification to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="specification"/> is <see langword="null"/>.</exception>
    public T FindFirst(ISpecification<T> specification, T defaultValue, DataSourceInfo dataSourceInfo) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      BusinessRuleSpecification<T> businessRule = specification as BusinessRuleSpecification<T>;
      if(businessRule != null) {
        return this.ExecuteBusinessRuleWithSingleResult(businessRule.RuleName, businessRule.RuleArguments);
      }

      Expression<Func<T, bool>> expression = specification.Visit(this);
      return this.FindFirstCore(expression, specification.SortRules, dataSourceInfo, defaultValue);
    }
    #endregion

    #region Dispose methods
    /// <summary>Disposes any resources held by this instance.</summary>
    public void Dispose() {
      this.DisposeManagedResources();
    }

    /// <summary>Disposes all the managed resources that are held by this instance.</summary>
    protected virtual void DisposeManagedResources() {
      /* By default there is nothing to dispose */
    }
    #endregion

    #region Extensibility methods
    /// <summary>Saves all the changes to the underlying persistency.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected abstract void SaveChangesCore(DataSourceInfo dataSourceInfo);

    /// <summary>Creates a new entity.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The newly created entity.</returns>
    protected abstract T CreateEntityCore(DataSourceInfo dataSourceInfo);

    /// <summary>Adds a new entity to the repository.</summary>
    /// <param name="entity">The entity that must be added to the repository.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T AddEntityCore(T entity, DataSourceInfo dataSourceInfo);

    /// <summary>Updates the repository with the changes made to the entity.</summary>
    /// <param name="entity">The entity whose members are updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T UpdateEntityCore(T entity, DataSourceInfo dataSourceInfo);

    /// <summary>Deletes the entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected abstract void DeleteEntityCore(T entity, DataSourceInfo dataSourceInfo);

    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected virtual void ResetCore(DataSourceInfo dataSourceInfo) {
    }

    /// <summary>Adds a collection of new entities to the repository.</summary>
    /// <param name="entities">The entities that must be added to the repository.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities with the most recent values.</returns>
    protected virtual IEnumerable<T> AddEntitiesCore(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      List<T> addedEntities = new List<T>(entities.Count());
      foreach(T entity in entities) {
        addedEntities.Add(this.AddEntityCore(entity, dataSourceInfo));
      }

      return addedEntities;
    }

    /// <summary>Updates the repository with the changes made to the entities.</summary>
    /// <param name="entities">The entities whose members have changed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities with the most recent values.</returns>
    protected virtual IEnumerable<T> UpdateEntitiesCore(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      List<T> updatedEntities = new List<T>(entities.Count());
      foreach(T entity in entities) {
        updatedEntities.Add(this.UpdateEntityCore(entity, dataSourceInfo));
      }

      return updatedEntities;
    }

    /// <summary>Deletes the entities from the repository.</summary>
    /// <param name="entities">The entities that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected virtual void DeleteEntitiesCore(IEnumerable<T> entities, DataSourceInfo dataSourceInfo) {
      foreach(T entity in entities) {
        this.DeleteEntityCore(entity, dataSourceInfo);
      }
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected virtual IEnumerable<T> FindAllCore(Expression<Func<T, bool>> expression, SortSpecifications<T> sortRules,
      int maximumResults, DataSourceInfo dataSourceInfo) {
      return this.FindAllCore(expression.Compile(), sortRules, maximumResults, dataSourceInfo);
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected virtual IEnumerable<T> FindAllCore(Func<T, bool> expression, SortSpecifications<T> sortRules,
      int maximumResults, DataSourceInfo dataSourceInfo) {
      return Enumerable.Empty<T>();
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected virtual T FindSingleCore(Expression<Func<T, bool>> expression, DataSourceInfo dataSourceInfo, T defaultValue) {
      return this.FindSingleCore(expression.Compile(), dataSourceInfo, defaultValue);
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected virtual T FindSingleCore(Func<T, bool> expression, DataSourceInfo dataSourceInfo, T defaultValue) {
      return null;
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected virtual T FindFirstCore(Expression<Func<T, bool>> expression, SortSpecifications<T> sortRules, DataSourceInfo dataSourceInfo,
      T defaultValue) {
      return this.FindFirstCore(expression.Compile(), sortRules, dataSourceInfo, defaultValue);
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected virtual T FindFirstCore(Func<T, bool> expression, SortSpecifications<T> sortRules, DataSourceInfo dataSourceInfo, T defaultValue) {
      return null;
    }

    /// <summary>Finds all the entities of type <typeparamref name="T"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>All the available entities.</returns>
    protected virtual IEnumerable<T> FindAllCore(DataSourceInfo dataSourceInfo) {
      Expression<Func<T, bool>> expression = t => true;
      return this.FindAllCore(expression, null, -1, dataSourceInfo);
    }

    /// <summary>Finds a single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    protected virtual T FindSingleCore(Expression<Func<T, bool>> expression, DataSourceInfo dataSourceInfo) {
      return this.FindSingleCore(expression, dataSourceInfo, null);
    }

    /// <summary>Finds the first single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    protected virtual T FindFirstCore(Expression<Func<T, bool>> expression, SortSpecifications<T> sortRules, DataSourceInfo dataSourceInfo) {
      return this.FindFirstCore(expression, sortRules, dataSourceInfo, null);
    }

    /// <summary>Creates an AND-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    protected virtual Expression<Func<T, bool>> CreateAndExpressionCore(ISpecification<T> leftOperand, ISpecification<T> rightOperand) {
      Expression<Func<T, bool>> leftExpression = leftOperand.Visit(this);
      Expression<Func<T, bool>> rightExpression = rightOperand.Visit(this);
      Expression<Func<T, bool>> andExpression = leftExpression.And(rightExpression);

      return andExpression;
    }

    /// <summary>Creates an OR-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
      Justification = "'or' represents the type of expression. However, because it is so specific, it is not added to the allowed-list")]
    protected virtual Expression<Func<T, bool>> CreateOrExpressionCore(ISpecification<T> leftOperand, ISpecification<T> rightOperand) {
      Expression<Func<T, bool>> leftExpression = leftOperand.Visit(this);
      Expression<Func<T, bool>> rightExpression = rightOperand.Visit(this);
      Expression<Func<T, bool>> orExpression = leftExpression.Or(rightExpression);

      return orExpression;
    }

    /// <summary>Creates a lambda-expression using the specified expression. Typically, this method simply returns the parameter.</summary>
    /// <param name="expression">The expression that was originally passed to the specification.</param>
    /// <returns>The created expression.</returns>
    protected virtual Expression<Func<T, bool>> CreateLambdaExpressionCore(Expression<Func<T, bool>> expression) {
      return expression;
    }

    /// <summary>Creates a NOT-expression using the specified specification.</summary>
    /// <param name="specification">The specification whose result must be inverted.</param>
    /// <returns>The created expression.</returns>
    protected virtual Expression<Func<T, bool>> CreateNotExpressionCore(ISpecification<T> specification) {
      Expression<Func<T, bool>> expr = specification.Visit(this);
      Expression<Func<T, bool>> notExpression = expr.Not();

      return notExpression;
    }

    /// <summary>Creates a LIKE-expression using the specified field and searchpattern.</summary>
    /// <param name="field">The field of type <c>T</c> that must match the pattern.</param>
    /// <param name="pattern">The pattern to which the field must apply. The pattern may contain a '*' and '?' wildcard.</param>
    /// <returns>The created expression.</returns>
    protected virtual Expression<Func<T, bool>> CreateLikeExpressionCore(Expression<Func<T, string>> field, string pattern) {
      pattern = pattern.Replace("*", ".*").Replace("?", ".?");
      Expression<Func<T, bool>> regexExpression = t => Regex.IsMatch(field.Compile()(t), pattern);

      return regexExpression;
    }

    /// <summary>Creates a lambda-expression using the custom specification. This method is executed when a specification-type is used that is not 
    /// part of the default specification system.</summary>
    /// <param name="specification">The custom specification.</param>
    /// <returns>The created expression.</returns>
    protected virtual Expression<Func<T, bool>> CreateCustomExpressionCore(ISpecification<T> specification) {
      throw new NotSupportedException("Specification-type {" + specification.GetType() + "} is not supported");
    }

    /// <summary>Executes a businessrule that yields to a single result. By default, this method throws a <see cref="NotSupportedException"/>. 
    /// Override this method to deal with special businessrules.</summary>
    /// <param name="ruleName">The name of the rule that must be executed.</param>
    /// <param name="ruleArguments">The arguments that were passed.</param>
    /// <returns>The result of the businessrule.</returns>
    protected virtual T ExecuteBusinessRuleWithSingleResult(string ruleName, IEnumerable<object> ruleArguments) {
      throw new NotSupportedException("This repository does not support businessrules.");
    }

    /// <summary>Executes a businessrule that yields to multiple results. By default, this method throws a <see cref="NotSupportedException"/>. 
    /// Override this method to deal with special businessrules.</summary>
    /// <param name="ruleName">The name of the rule that must be executed.</param>
    /// <param name="ruleArguments">The arguments that were passed.</param>
    /// <returns>The result of the businessrule.</returns>
    protected virtual IEnumerable<T> ExecuteBusinessRuleWithMultipleResults(string ruleName, IEnumerable<object> ruleArguments) {
      throw new NotSupportedException("This repository does not support businessrules.");
    }

    /// <summary>Executes an action in the repository as specified by <paramref name="specification"/>.</summary>
    /// <typeparam name="TResult">The type of object that is returned by the action.</typeparam>
    /// <param name="specification">The specification that indicates the action that must be executed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>An object describing the result of the action.</returns>
    protected virtual TResult ExecuteCore<TResult>(ISpecification<T> specification, DataSourceInfo dataSourceInfo) {
      throw new NotSupportedException("This repository does not support the Execute-operation.");
    }
    #endregion

    #region Protected helper methods
    /// <summary>Selects the flag that indicates if any entity that originates from the data source must be cloned or not. If 
    /// <paramref name="dataSourceInfo"/> contains a valid flag, it is used; otherwise the value of <see cref="CloneDataSourceItems"/> is used.
    /// </summary>
    /// <param name="dataSourceInfo">Any information regarding the datasource.</param>
    /// <exception cref="InvalidOperationException"><paramref name="dataSourceInfo"/> indicates that entities must be cloned, but 
    /// <typeparamref name="T"/> does not implement <see cref="ICloneable"/>.</exception>
    /// <returns><see langword="true"/> if entities must be cloned; <see langword="false"/> otherwise.</returns>
    protected bool SelectCloneDataSourceItems(DataSourceInfo dataSourceInfo) {
      if(DataSourceInfo.IsCloneDataSourceItemsSpecified(dataSourceInfo)) {
        bool useClone = DataSourceInfo.SelectCloneDataSourceItems(dataSourceInfo);
        if(useClone && !this.typeImplementsICloneable) {
          throw new InvalidOperationException("Cannot clone data source items because the type does not implement ICloneable.");
        }
        else {
          return useClone;
        }
      }
      else {
        return this.CloneDataSourceItems;
      }
    }
    #endregion
  }
}
