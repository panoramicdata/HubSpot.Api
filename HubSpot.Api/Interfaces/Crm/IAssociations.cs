using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IAssociations
{
	[Post("/crm/v3/associations/contacts/companies/batch/read")]
	Task<GetAssociationResponse> GetContactToCompanyAssociations(
		[Body] GetAssociationsFor contactAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/companies/contacts/batch/read")]
	Task<GetAssociationResponse> GetCompanyToContactAssociations(
		[Body] GetAssociationsFor companyAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/deals/companies/batch/read")]
	Task<GetAssociationResponse> GetDealToCompanyAssociations(
		[Body] GetAssociationsFor dealAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/companies/deals/batch/read")]
	Task<GetAssociationResponse> GetCompanyToDealAssociations(
		[Body] GetAssociationsFor companyAssociationsFor,
		CancellationToken cancellationToken = default);
}