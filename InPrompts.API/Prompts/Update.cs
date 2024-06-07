using Ardalis.Result;
using Ardalis.SharedKernel;
using FastEndpoints;
using InPrompts.Core;
using InPrompts.UseCases;
using MediatR;

namespace InPrompts.API.Prompts;

/// <summary>
/// Update an existing Prompt.
/// </summary>
/// <remarks>
/// Update an existing Prompt by providing a fully defined replacement set of values.
/// See: https://stackoverflow.com/questions/60761955/rest-update-best-practice-put-collection-id-without-id-in-body-vs-put-collecti
/// </remarks>
public class Update : Endpoint<UpdatePromptRequest, UpdatePromptResponse>
{
  private readonly IRepository<Prompt> _repository;
  private readonly IMediator _mediator;

  public Update(IRepository<Prompt> repository, IMediator mediator)
  {
    _repository = repository;
    _mediator = mediator;
  }

  public override void Configure()
  {
    Put(UpdatePromptRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdatePromptRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new UpdatePromptCommand(request.Id, request.Text!));

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    // TODO: Use Mediator
    var existingPrompt = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (existingPrompt == null)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      var dto = result.Value;
      Response = new UpdatePromptResponse(new PromptRecord(dto.Id, dto.Text, dto.Views));
      return;
    }
  }
}
