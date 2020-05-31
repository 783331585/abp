﻿using System;
using System.Linq.Expressions;
using AutoMapper;
using Volo.Abp.Auditing;

namespace Volo.Abp.AutoMapper
{
    public static class AutoMapperExpressionExtensions
    {
        public static IMappingExpression<TDestination, TMember> Ignore<TDestination, TMember, TResult>(this IMappingExpression<TDestination, TMember> mappingExpression, Expression<Func<TMember, TResult>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, opts => opts.Ignore());
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreHasCreationTimeProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IHasCreationTime
        {
            return mappingExpression.Ignore(x => x.CreationTime);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreMayHaveCreatorProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IMayHaveCreator
        {
            return mappingExpression.Ignore(x => x.CreatorId);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreCreationAuditedObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : ICreationAuditedObject
        {
            return mappingExpression
                .IgnoreHasCreationTimeProperties()
                .IgnoreMayHaveCreatorProperties();
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreHasModificationTimeProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IHasModificationTime
        {
            return mappingExpression.Ignore(x => x.LastModificationTime);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreModificationAuditedObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IModificationAuditedObject
        {
            return mappingExpression
                .IgnoreHasModificationTimeProperties()
                .Ignore(x => x.LastModifierId);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreAuditedObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IAuditedObject
        {
            return mappingExpression
                .IgnoreCreationAuditedObjectProperties()
                .IgnoreModificationAuditedObjectProperties();
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreSoftDeleteProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : ISoftDelete
        {
            return mappingExpression.Ignore(x => x.IsDeleted);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreHasDeletionTimeProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IHasDeletionTime
        {
            return mappingExpression
                .IgnoreSoftDeleteProperties()
                .Ignore(x => x.DeletionTime);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreDeletionAuditedObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IDeletionAuditedObject
        {
            return mappingExpression
                .IgnoreHasDeletionTimeProperties()
                .Ignore(x => x.DeleterId);
        }
        
        public static IMappingExpression<TSource, TDestination> IgnoreFullAuditedObjectProperties<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TDestination : IFullAuditedObject
        {
            return mappingExpression
                .IgnoreAuditedObjectProperties()
                .IgnoreDeletionAuditedObjectProperties();
        }
    }
}
