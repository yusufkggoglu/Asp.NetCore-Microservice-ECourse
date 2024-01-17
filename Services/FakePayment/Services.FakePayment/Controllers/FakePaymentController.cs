using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.FakePayment.Model;
using Shared.ControllerBases;
using Shared.Dtos;
using Shared.Messages;
using System;
using System.Threading.Tasks;

namespace Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(FakePaymentDto fakePaymentDto)
        {
            // ödeme işlemi gerçekleştir
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:created-order-service"));
            var createdOrderMessageCommand = new CreateOrderMessageCommand();
            createdOrderMessageCommand.BuyerId = fakePaymentDto.Order.BuyerId;
            createdOrderMessageCommand.Province = fakePaymentDto.Order.Address.Province;
            createdOrderMessageCommand.District = fakePaymentDto.Order.Address.District;
            createdOrderMessageCommand.Street = fakePaymentDto.Order.Address.Street;
            createdOrderMessageCommand.Line = fakePaymentDto.Order.Address.Line;
            createdOrderMessageCommand.ZipCode = fakePaymentDto.Order.Address.ZipCode;

            fakePaymentDto.Order.OrderItems.ForEach(x =>
            {
                createdOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });
            await sendEndpoint.Send<CreateOrderMessageCommand>(createdOrderMessageCommand);
            return CreateActionResultInstance<NoContent>(Shared.Dtos.Response<NoContent>.Success(200));
        }


    }
}
