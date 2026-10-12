# Redemption Creation Test Cases

Endpoint: `POST /api/Redemptions`

 Test Case 1: Successful Redemption

- What you do: Create a redemption for an active participant with sufficient points and an active reward.
- Input: Participant ID: `1`, Reward ID: `10`. The participant has 2,000 points, and the reward costs 500 points.
- Expected result: The API returns `200 OK` or `201 Created`, depending on the implementation. The redemption is created with `Pending` status, and the participant's balance decreases to 1,500 points.

 Test Case 2: Invalid Participant ID

- What you do: Submit a redemption with an invalid participant ID.
- Input: Participant ID: `0`, Reward ID: `10`.
- Expected result: The API returns `400 Bad Request`. No redemption is created, and no points are deducted.

 Test Case 3: Participant Does Not Exist

- What you do: Submit a redemption for a participant who does not exist.
- Input: Participant ID: `9999`, Reward ID: `10`.
- Expected result: The API returns `404 Not Found`. No redemption is created, and no points are deducted.

 Test Case 4: Inactive Participant

- What you do: Attempt to create a redemption for an inactive participant.
- Input: An inactive participant ID and Reward ID: `10`.
- Expected result: The API returns `400 Bad Request`. No redemption is created, and no points are deducted.

 Test Case 5: Reward Does Not Exist

- What you do: Submit a redemption using a reward ID that does not exist.
- Input: Participant ID: `1`, Reward ID: `9999`.
- Expected result: The API returns `404 Not Found`. No redemption is created, and no points are deducted.

 Test Case 6: Inactive Reward

- What you do: Attempt to redeem an inactive reward.
- Input: Participant ID: `1`, Reward ID: `12` (Legacy Gift, inactive).
- Expected result: The API returns `400 Bad Request`. No redemption is created, and no points are deducted.

 Test Case 7: Insufficient Points

- What you do: Attempt to redeem a reward that costs more points than the participant has.
- Input: A participant with 200 points and Reward ID: `10` (costs 500 points).
- Expected result: The API returns `400 Bad Request`. No redemption is created, and the participant's balance remains unchanged.

 Test Case 8: Duplicate Pending Redemption

- What you do: Attempt to redeem the same reward while the participant already has a pending redemption for it.
- Input: Participant ID: `2`, Reward ID: `10`, with an existing pending redemption for that combination.
- Expected result: The API returns `409 Conflict`. No additional redemption is created, and no points are deducted.

 Test Case 9: Exact Points Balance (Edge Case)

- What you do: Create a redemption when the participant has exactly enough points to redeem the reward.
- Input: A participant with 500 points and Reward ID: `10` (costs 500 points).
- Expected result: The redemption is created with `Pending` status, and the participant's balance becomes 0.

 Test Case 10: Missing Required Input (Edge Case)

- What you do: Submit a redemption without a participant ID.
- Input:

  ```json
  {
    "rewardId": 10
  }
  ```

- Expected result: The API returns `400 Bad Request`. No redemption is created, and no points are deducted.