﻿using System;
using System.ComponentModel.DataAnnotations;
using RawRabbit.Attributes;
using RawRabbit.Configuration.Exchange;

namespace FDP.OrderService.MessageDirectory.Message
{
    [Exchange(Type = ExchangeType.Topic, Name = "message.exchange")]
    [Queue(Name = "FDP.OrderService.MessageDirectory:Message.RestaurantCreated")]
    [Routing(RoutingKey = "FDP.OrderService.MessageDirectory:Message.RestaurantCreated", PrefetchCount = 50)]
    public class RestaurantCreated
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String Code { get; set; } 
    }
}
