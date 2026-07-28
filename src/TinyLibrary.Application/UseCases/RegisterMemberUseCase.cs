using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Application.UseCases
{
    public class RegisterMemberUseCase
    {

        private IUnitOfWork unitOfWork;
        private IMemberRepository memberRepository;
        private TimeProvider timeProvider;

        public RegisterMemberUseCase(IUnitOfWork _unitOfWork, IMemberRepository _memberRepository, TimeProvider _timeProvider)
        {
            unitOfWork = _unitOfWork;
            memberRepository = _memberRepository;
            timeProvider = _timeProvider;
        }

        public async Task<MemberDto> ExecuteAsync(RegisterMemberRequest request, CancellationToken cancellationToken)
        {
            var existingMember = await memberRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingMember != null)
            {
                throw new DuplicateEmailMemberException(existingMember.Email);
            }

            var now = timeProvider.GetUtcNow();
            var member = new Member(request.Name, request.Email, now);

            memberRepository.Add(member);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new MemberDto(
                member.Id,
                member.Name,
                member.Email,
                member.JoinedAt,
                member.PenalizedUntil
            );
        }
    }
}
