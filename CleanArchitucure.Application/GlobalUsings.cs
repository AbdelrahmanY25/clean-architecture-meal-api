global using System.Linq.Expressions;


global using CleanArchitucure.Application.Mapping;
global using CleanArchitucure.Application.Services;
global using CleanArchitucure.Application.Common.Errors;
global using CleanArchitucure.Application.Specifications;
global using CleanArchitucure.Application.Common.Abstractions;
global using CleanArchitucure.Application.Interfaces.Services;
global using CleanArchitucure.Application.Interfaces.Persistence;
global using CleanArchitucure.Application.Contracts.Meals.Requests;
global using CleanArchitucure.Application.Contracts.Meals.Response;
global using CleanArchitucure.Application.Contracts.MealOptions.Requests;
global using CleanArchitucure.Application.Contracts.OptionItems.Requests;
global using CleanArchitucure.Application.Contracts.MealOptions.Responses;
global using CleanArchitucure.Application.Contracts.OptionItems.Responses;
global using CleanArchitucure.Application.Contracts.MealOptions.Validators;
global using CleanArchitucure.Application.Contracts.OptionItems.Validators;
global using CleanArchitucure.Application.Specifications.MealsSpecifications;


global using CleanArchitucure.Domain.Entities;


global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;



global using Mapster;
global using MapsterMapper;
global using FluentValidation;
global using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;