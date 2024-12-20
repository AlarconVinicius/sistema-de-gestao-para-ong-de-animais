﻿using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using FluentValidation;
using FluentValidation.Results;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Shared.Entities;

namespace SGONGA.WebAPI.Business.Shared.Handlers;

public class BaseHandler
{
    private readonly INotifier _notifier;
    protected ValidationResult _validationResult;
    protected List<Error> _errorsList;
    public readonly IAspNetUser AppUser;
    protected Guid UserId { get; set; }
    protected Guid TenantId = Guid.Empty;
    protected bool IsUserAuthenticated { get; set; }

    protected BaseHandler(INotifier notifier, IAspNetUser appUser)
    {
        _notifier = notifier;
        _validationResult = new ValidationResult();
        _errorsList = new();

        AppUser = appUser;

        if (appUser.IsAuthenticated())
        {
            UserId = appUser.GetUserId();
            IsUserAuthenticated = true;
            TenantId = appUser.GetTenantId() != Guid.Empty ? appUser.GetTenantId() : Guid.Empty;
        }
    }

    protected void Notify(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notify(error.ErrorMessage);
        }
    }

    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
        _validationResult.Errors.Add(new ValidationFailure(string.Empty, message));

        _errorsList.Add(Error.Validation("VALIDATION", message));
    }
    protected bool IsOperationValid()
    {
        return !_notifier.HasNotification();
    }
    protected void ClearNotifications()
    {
        _notifier.Clear();
        return;
    }
    protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notify(validator);

        return false;
    }

    protected bool TenantIsEmpty()
    {
        if (TenantId == Guid.Empty)
        {
            Notify("TenantId não encontrado.");
            return true;
        }
        return false;
    }
}