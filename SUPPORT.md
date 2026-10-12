# Support Ticket: Timo's R100 Voucher Redemption

 1. Problem

Timo Negonga from Namibia reports that he cannot redeem the R100 voucher again. He also wants to know why his previous request is still showing as Pending.

 2. Investigation

First, check the `Redemptions` table for Timo's voucher request. Confirm the redemption ID, participant ID, reward ID, request date, and current status.

Next, check whether the system has recorded an error for the transaction. Review the application logs for provider timeouts, failed requests, or other errors related to the redemption.

If the database shows the request is still Pending but there is no clear explanation, check the voucher provider's platform or contact the provider to confirm whether the voucher was issued.

 3. Possible Cause

The system prevents a participant from submitting another redemption for the same reward while an existing request is Pending. This could explain why Timo is receiving an error when trying again.

However, the reason the original request has not completed must still be confirmed. It could be awaiting provider processing or may have encountered an error.

 4. Resolution

- If the provider confirms that the voucher was issued, update the redemption status to `Completed`.
- If the provider confirms that the transaction failed, record the failure reason, update the status to `Failed`, and refund the points according to the system's rules.
- If the provider cannot confirm the outcome, keep the request under investigation and escalate it to the development team or provider support.
- Do not create another redemption until the original transaction has been checked, to avoid duplicate vouchers or incorrect points balances.

 5. Customer Update

Inform Timo that the team is checking his original request and will update him once the transaction has been verified.

 6. Ticket Status

Open – pending investigation of the original voucher transaction.