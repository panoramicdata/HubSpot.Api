using HubSpot.Api.Models.Crm;
using Refit;

namespace HubSpot.Api.Interfaces.Crm;

public interface IAssociations
{
	#region Contact To XXX Associations

	[Post("/crm/v3/associations/contacts/companies/batch/read")]
	Task<GetAssociationResponse> GetContactToCompanyAssociations(
		[Body] GetAssociationsFor contactAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/contacts/deals/batch/read")]
	Task<GetAssociationResponse> GetContactToDealAssociations(
		[Body] GetAssociationsFor contactAssociationsFor,
		CancellationToken cancellationToken = default);

	#endregion

	#region Company To XXX Associations

	[Post("/crm/v3/associations/companies/contacts/batch/read")]
	Task<GetAssociationResponse> GetCompanyToContactAssociations(
		[Body] GetAssociationsFor companyAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/companies/deals/batch/read")]
	Task<GetAssociationResponse> GetCompanyToDealAssociations(
		[Body] GetAssociationsFor companyAssociationsFor,
		CancellationToken cancellationToken = default);

	#endregion

	#region Deal To XXX Associations

	[Post("/crm/v3/associations/deals/companies/batch/read")]
	Task<GetAssociationResponse> GetDealToCompanyAssociations(
		[Body] GetAssociationsFor dealAssociationsFor,
		CancellationToken cancellationToken = default);

	[Post("/crm/v3/associations/deals/contacts/batch/read")]
	Task<GetAssociationResponse> GetDealToContactAssociations(
		[Body] GetAssociationsFor dealAssociationsFor,
		CancellationToken cancellationToken = default);

	#endregion
}