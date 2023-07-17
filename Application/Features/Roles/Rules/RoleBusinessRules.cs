using Application.IRepositories.EntityRepositories;

namespace Application.Features.Roles.Rules
{
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
            if (result != null) throw new Exception("Role name exists.");
            // if (result != null) throw new BusinessException("Role name exists.");
        }

        // public void RoleShouldExistWhenRequested(Brand brand)
        // {
        //     if (brand == null) throw new BusinessException("Requested brand does not exist");
        // }
    }
}
