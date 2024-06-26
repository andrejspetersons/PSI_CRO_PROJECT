﻿using Newtonsoft.Json;
using System;

/// <summary>
/// Summary description for PaymentBillViewModel
/// </summary>
/// 
[Serializable]
public class PaymentBillUserViewModel
{
    [JsonProperty("paymentBillId")]
    public int PaymentBillId { get; set; }
    [JsonProperty("serviceName")]
    public string ServiceName { get; set; }
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    [JsonProperty("issueDate")]
    public DateTime IssueDate { get; set; }
    [JsonProperty("dueToDate")]
    public DateTime DueToDate { get; set; }
    [JsonProperty("paymentDate")]
    public DateTime? PaymentDate { get; set; }
    [JsonProperty("paymentReceipt")]
    public int? PaymentReceipt { get; set; }
    [JsonProperty("paymentStatus")]
    public string PaymentStatus { get; set; }

}