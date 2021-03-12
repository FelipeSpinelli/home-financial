using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IRevenueRepository _revenueRepository;

        public MemberService
        (
            IMemberRepository memberRepository, 
            IAccountRepository accountRepository, 
            ICreditCardRepository creditCardRepository, 
            IRevenueRepository revenueRepository
        )
        {
            _memberRepository = memberRepository;
            _accountRepository = accountRepository;
            _creditCardRepository = creditCardRepository;
            _revenueRepository = revenueRepository;
        }

        public async Task<IEnumerable<Member>> GetAll() => await _memberRepository.GetAll();

        public async Task<Member> GetById(string id)
        {
            var member = await _memberRepository.GetById(id);
            return  member ?? throw new NullReferenceException("Invalid Member!");
        }

        public async Task Upsert(Member member, string id = default)
        {
            if (id == default)
            {
                await _memberRepository.Insert(member);
                return;
            }

            var currentMember = await _memberRepository.GetById(id);
            if (currentMember == null || currentMember.Id != member.Id)
            {
                throw new NullReferenceException("Invalid Member!");
            }

            await _memberRepository.Update(id, member);
        }

        public async Task AddAccount(string memberId, Account account)
        {
            var member = await GetById(memberId);
            account.SetOwner(member);
            await _accountRepository.Insert(account);
        }

        public async Task AddCreditCard(string memberId, CreditCard creditCard)
        {
            var member = await GetById(memberId);
            creditCard.SetHolder(member);
            await _creditCardRepository.Insert(creditCard);
        }

        public async Task AddRevenue(string accountId, Revenue revenue)
        {
            var account = await _accountRepository.GetById(accountId);
            
            if(account == null)
            {
                throw new NullReferenceException("Invalid Account!");
            }

            revenue.SetTarget(account);
            await _revenueRepository.Insert(revenue);
        }
    }
}
