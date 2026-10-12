<script setup lang="ts">
import { RedemptionStatus } from '../types';
import type { Redemption } from '../types';

defineProps<{
  redemptions: Redemption[];
}>();

const emit = defineEmits<{
  complete: [id: number];
  fail: [id: number, reason: string];
}>();

function askFail(id: number) {
  const reason = window.prompt('Failure reason:');

  if (reason && reason.trim()) {
    emit('fail', id, reason.trim());
  }
}
</script>

<template>
  <div class="table-container">
    <table>
      <thead>
        <tr>
          <th>Participant</th>
          <th>Country</th>
          <th>Reward</th>
          <th>Points</th>
          <th>Status</th>
          <th>Requested Date</th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="redemption in redemptions" :key="redemption.redemptionId">
          <td>{{ redemption.participantName }}</td>
          <td>{{ redemption.countryCode }}</td>
          <td>{{ redemption.rewardName }}</td>
          <td>{{ redemption.pointsCost }}</td>
          <td>{{ redemption.status }}</td>
          <td>{{ redemption.requestedAt }}</td>
        </tr>

        <tr v-if="redemptions.length === 0">
          <td colspan="7">
            No redemptions found.
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>