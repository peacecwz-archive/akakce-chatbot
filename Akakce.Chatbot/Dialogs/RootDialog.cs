using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Akakce.Chatbot.Services;
using System.Linq;
using System.Collections.Generic;

namespace Akakce.Chatbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text.IndexOf("Data: ") > -1)
                await context.PostAsync(await GetDetailReply(context));
            else
                await context.PostAsync(await SearchReply(context));

            context.Wait(MessageReceivedAsync);
        }

        private async Task<IMessageActivity> SearchReply(IDialogContext context)
        {
            var activity = context.Activity as Activity;

            var reply = context.MakeMessage();

            var result = await ServiceContext.Instance.Search(activity.Text);
            if (result == null)
                reply.Text = "Malesef ki söylediğiniz anlayabileceğim şeyler değil!";
            else if (result.Count == 0)
                reply.Text = "Üzgünüm ki hiçbir sonuç bulamadım";
            else
            {
                reply.Attachments = result.Select(x => GetHeroCard(x.Name,
                                                                    "",
                                                                    "",
                                                                    new CardImage(url: x.ImageUrl),
                                                                    new CardAction(type: ActionTypes.PostBack, title: "Satıcıları Görüntüle", image: "", value: $"Data: {x.ProductUrl}")
                                                                  )).ToList();
                reply.AttachmentLayout = AttachmentLayoutTypes.List;
            }
            return reply;
        }

        private async Task<IMessageActivity> GetDetailReply(IDialogContext context)
        {
            var activity = context.Activity as Activity;

            var reply = context.MakeMessage();

            var result = await ServiceContext.Instance.Get(activity.Text.Replace("Data: ", ""));
            if (result == null)
                reply.Text = "Malesef ki bir ürün seçildiğinden emin değilim!";
            else if (result.Count == 0)
                reply.Text = "Üzgünüm ki hiçbir sonuç bulamadım";
            else
            {
                reply.Attachments = result.Select(x => GetHeroCard(x.Name,
                                                                    $"{x.Seller} - {x.Price}",
                                                                    "",
                                                                    new CardImage(url: x.ImageUrl),
                                                                    new CardAction(type: ActionTypes.OpenUrl, title: "Siteye git", image: "", value: x.Url)
                                                                  )).ToList();
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            }
            return reply;
        }

        private Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
    }
}