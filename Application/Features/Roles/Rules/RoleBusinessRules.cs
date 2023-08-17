using Application.IRepositories.Derived;
using Application.PipelineBehaviors;

namespace Application.Features.Roles.Rules;

public class RoleBusinessRules
{
    private readonly IRoleRepository _roleRepository;

    public RoleBusinessRules(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task RoleNameCanNotBeDuplicatedWhenInserted(string name)
    {
        var result = await _roleRepository.Get(x => x.Name == name);
        if (result != null)
            throw new BusinessException("Role name exists.");
    }
}