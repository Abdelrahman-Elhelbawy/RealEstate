using FluentValidation;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public class AgentService : IAgentService
{
    private readonly IAgentRepository _agentRepository;

    private readonly IValidator<CreateAgentDto> _createValidator;

    private readonly IValidator<UpdateAgentDto> _updateValidator;

    public AgentService(
        IAgentRepository agentRepository,
        IValidator<CreateAgentDto> createValidator,
        IValidator<UpdateAgentDto> updateValidator)
    {
        _agentRepository = agentRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<AgentListDto>> GetAllAsync()
    {
        var agents = await _agentRepository.GetAllAsync();

        return agents.Select(a => new AgentListDto
        {
            Id = a.Id,
            Name = a.Name,
            Phone = a.Phone,
            WhatsApp = a.WhatsApp,
            Email = a.Email,
            PropertiesCount = a.properties.Count
        }).ToList();
    }

    public async Task<AgentDto?> GetByIdAsync(int id)
    {
        var agent = await _agentRepository.GetByIdAsync(id);

        if (agent is null)
            return null;

        return new AgentDto
        {
            Id = agent.Id,
            Name = agent.Name,
            Phone = agent.Phone,
            WhatsApp = agent.WhatsApp,
            Email = agent.Email
        };
    }

    public async Task<AgentDto?> GetByIdWithPropertiesAsync(int id)
    {
        var agent = await _agentRepository
            .GetByIdWithPropertiesAsync(id);

        if (agent is null)
            return null;

        return new AgentDto
        {
            Id = agent.Id,
            Name = agent.Name,
            Phone = agent.Phone,
            WhatsApp = agent.WhatsApp,
            Email = agent.Email,
            PropertiesCount = agent.properties.Count
        };
    }

    public async Task<int> CreateAsync(CreateAgentDto dto)
    {
        var validationResult =
            await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                validationResult.Errors);
        }

        var agent = new Agent
        {
            Name = dto.Name,
            Phone = dto.Phone,
            WhatsApp = dto.WhatsApp,
            Email = dto.Email
        };

        await _agentRepository.AddAsync(agent);

        await _agentRepository.SaveChangesAsync();

        return agent.Id;
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateAgentDto dto)
    {
        var validationResult =
            await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                validationResult.Errors);
        }

        var agent = await _agentRepository.GetByIdAsync(id);

        if (agent is null)
            return false;

        agent.Name = dto.Name;
        agent.Phone = dto.Phone;
        agent.WhatsApp = dto.WhatsApp;
        agent.Email = dto.Email;

        _agentRepository.Update(agent);

        await _agentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var agent = await _agentRepository.GetByIdAsync(id);

        if (agent is null)
            return false;

        var hasProperties =
            await _agentRepository.HasPropertiesAsync(id);

        if (hasProperties)
            return false;

        _agentRepository.Delete(agent);

        await _agentRepository.SaveChangesAsync();

        return true;
    }
}