using FluentValidation;
using RealEstate.Application.DTOs.Message;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    private readonly IValidator<CreateMessageDto> _createValidator;

    private readonly IValidator<UpdateMessageDto> _updateValidator;

    public MessageService(
        IMessageRepository messageRepository,
        IValidator<CreateMessageDto> createValidator,
        IValidator<UpdateMessageDto> updateValidator)
    {
        _messageRepository = messageRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<MessageListDto>> GetAllAsync()
    {
        var messages = await _messageRepository.GetAllAsync();

        return messages.Select(m => new MessageListDto
        {
            Id = m.Id,
            SenderName = m.SenderName,
            SenderPhone = m.SenderPhone,
            SenderEmail = m.SenderEmail,
            Content = m.Content,
            IsRead = m.IsRead,
            PropertyId = m.PropertyId,
            PropertyTitle = m.Property?.Title
        }).ToList();
    }

    public async Task<MessageDto?> GetByIdAsync(int id)
    {
        var message = await _messageRepository.GetByIdAsync(id);

        if (message is null)
            return null;

        return new MessageDto
        {
            Id = message.Id,
            SenderName = message.SenderName,
            SenderPhone = message.SenderPhone,
            SenderEmail = message.SenderEmail,
            Content = message.Content,
            IsRead = message.IsRead,
            PropertyId = message.PropertyId,
            PropertyTitle = message.Property?.Title
        };
    }

    public async Task<List<MessageListDto>> GetByPropertyIdAsync(int propertyId)
    {
        var messages =
            await _messageRepository.GetByPropertyIdAsync(propertyId);

        return messages.Select(m => new MessageListDto
        {
            Id = m.Id,
            SenderName = m.SenderName,
            SenderPhone = m.SenderPhone,
            SenderEmail = m.SenderEmail,
            Content = m.Content,
            IsRead = m.IsRead,
            PropertyId = m.PropertyId,
            PropertyTitle = m.Property?.Title
        }).ToList();
    }

    public async Task<int> CreateAsync(CreateMessageDto dto)
    {
        var validationResult =
            await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var propertyExists =
            await _messageRepository.PropertyExistsAsync(dto.PropertyId);

        if (!propertyExists)
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");

        var message = new Message
        {
            SenderName = dto.SenderName,
            SenderPhone = dto.SenderPhone,
            SenderEmail = dto.SenderEmail,
            Content = dto.Content,
            IsRead = false,
            PropertyId = dto.PropertyId
        };

        await _messageRepository.AddAsync(message);

        await _messageRepository.SaveChangesAsync();

        return message.Id;
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateMessageDto dto)
    {
        var validationResult =
            await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var message =
            await _messageRepository.GetByIdAsync(id);

        if (message is null)
            return false;

        var propertyExists =
            await _messageRepository.PropertyExistsAsync(dto.PropertyId);

        if (!propertyExists)
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");

        message.SenderName = dto.SenderName;
        message.SenderPhone = dto.SenderPhone;
        message.SenderEmail = dto.SenderEmail;
        message.Content = dto.Content;
        message.IsRead = dto.IsRead;
        message.PropertyId = dto.PropertyId;

        _messageRepository.Update(message);

        await _messageRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var message =
            await _messageRepository.GetByIdAsync(id);

        if (message is null)
            return false;

        _messageRepository.Delete(message);

        await _messageRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var message =
            await _messageRepository.GetByIdAsync(id);

        if (message is null)
            return false;

        message.IsRead = true;

        _messageRepository.Update(message);

        await _messageRepository.SaveChangesAsync();

        return true;
    }
}