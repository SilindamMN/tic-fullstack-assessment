 Support Ticket: Timo's R100 Voucher Redemption

 1. Issue Description

Timo Negonga from Namibia reports that he cannot redeem the R100 voucher again because the system returns an error. He also wants to know why his previous voucher request, submitted on the 8th, has not been marked as completed.

The issue needs to be investigated to establish why the original redemption remains Pending and whether it is safe to allow another attempt.

 2. Investigation Steps

 Step 1: Check Timo's participant record

Query the `Participants` table using Timo's participant ID or full name.

Check the following:
- Participant ID and full name.
- Country code.
- Whether the participant is active.
- Current points balance.

Expected finding: The seed data identifies Timo Negonga as a participant from Namibia, with country code `NA`. Confirm his current points balance and active status directly from the database before proceeding.

 Step 2: Check the reward

Query the `Rewards` table for the R100 voucher.

Check:
- Reward ID and name.
- Points cost.
- Whether the reward is active.

Expected finding: The seed data contains the R100 voucher as reward ID `11`, costing 1,000 points, and marked active. The API should reject a redemption if the reward is inactive or the participant does not have enough points.

 Step 3: Review Timo's redemption history

Query the `Redemptions` table using Timo's participant ID. Filter the results to the R100 voucher and order them by request date, newest first.

Check the redemption ID, reward ID, status, request timestamp, and failure reason if one exists.

Expected finding: The seed data contains redemption ID `101` for Timo and reward ID `11`. Its status is `Pending`, with a request date of 8 September 2026.

Confirm that this is the request Timo means by the voucher he requested on the 8th. If the database has changed since the seed data was created, use the current record rather than assuming the original status is unchanged.

 Step 4: Determine why the retry is rejected

Review the API response and the application's redemption rules.

The API checks whether a Pending redemption already exists for the same participant and reward. If one exists, another request should be rejected with HTTP `409 Conflict`.

Expected finding: If redemption `101` is still Pending, this rule explains why Timo cannot submit another request for the same R100 voucher. The API is preventing a possible duplicate redemption.

Capture the actual response body and timestamp of Timo's failed attempt to confirm that this is the error being returned.

 Step 5: Investigate the pending status

Check the application logs for the original redemption and the attempted retry. Look for:
- Provider request and response details.
- Timeout or connection errors.
- Failed callbacks or status updates.
- Exceptions recorded when processing the redemption.

Next, check the voucher provider's platform or contact its support team to establish whether the voucher was issued, rejected, or remains in processing.

Expected finding: The seed data alone does not establish why the voucher remains Pending. It contains a separate failed airtime redemption, ID `102`, with the reason `Provider timeout`. This shows that a provider timeout exists in the sample data, but it does not prove that Timo's voucher experienced the same issue.

 Step 6: Resolve the original transaction

Use the provider's confirmed result before changing the redemption status.

- If the voucher was issued: Update the redemption to `Completed`. Do not deduct points again.
- If the voucher failed: Update the redemption to `Failed`, record the failure reason, and refund the points according to the application's business rules.
- If the provider's result is unknown: Keep the case open and escalate it. Do not create another redemption or refund points until the original transaction has been reconciled.

After resolving the original request, confirm that Timo's redemption status and points balance are correct.

 Step 7: Verify the fix

Recheck the redemption through the API and database.

Confirm that:
- The original redemption has the correct final status.
- Any refund has been applied only when appropriate.
- The points balance is correct.
- A retry is allowed only when the original request has been resolved and the business rules permit it.

Record the outcome and communicate it to the ops person so they can update Timo.

 3. Reply to the Ops Person

Hi,

I checked the sample data and found that Timo has a Pending request for the R100 voucher dated 8 September 2026. The system prevents him from requesting the same voucher again while that request is still pending, which could explain the error he is receiving.

The next step is to check the current redemption record and application logs, then confirm with the voucher provider whether the original transaction was completed or failed. The sample data contains a separate failed airtime request caused by a provider timeout, but I cannot confirm that this is related to Timo's voucher.

Once the original transaction is verified, we can update its status and correct the points balance if necessary. We should avoid submitting another request until we know what happened to the first one.

I'll escalate the case if the provider's response or the reason for the pending status cannot be established.

 4. Escalation Note to Senior Developer

Issue: Timo's R100 voucher redemption remains Pending, and attempts to redeem the same reward again are rejected.

Evidence gathered:
- The seed data identifies Timo Negonga as a participant from Namibia (`NA`).
- Redemption ID `101` links Timo to reward ID `11`, the R100 voucher.
- Redemption `101` is recorded as Pending, with a request date of 8 September 2026.
- The API rules reject another redemption for the same participant and reward while an existing redemption is Pending, returning HTTP `409 Conflict`.
- Redemption ID `102` is a separate failed airtime request with the reason `Provider timeout`. There is no evidence yet linking that failure to the voucher request.
- The cause of the voucher's pending status has not been confirmed.

Investigation requested:

Please review the provider integration and redemption status-update flow. Confirm whether the provider's response was received and persisted, whether any callback or background processing failed, and whether pending transactions are monitored or reconciled when a provider response is missing.

Please also confirm the safe recovery procedure for this transaction so that we do not issue a duplicate voucher or deduct or refund points incorrectly.

Current status: Open — awaiting verification of the original transaction.

 5. Outstanding Information

The following items must be confirmed before closing the ticket:

- The current status of redemption `101`.
- The actual HTTP status and error message returned when Timo retries.
- Any relevant application or provider error logs.
- Confirmation from the provider about whether the voucher was issued.
- The final redemption status and points balance after resolution.