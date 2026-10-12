<script setup lang="ts">
import { computed, ref, watch } from 'vue';
import type { Participant, Reward } from '../types';

const props = defineProps<{
  participants: Participant[];
  rewards: Reward[];
  loading: boolean;
  error: string;
  success: string;
}>();

const emit = defineEmits<{
  submit: [participantId: number, rewardId: number];
}>();

const selectedParticipantId = ref<number>(0);
const selectedRewardId = ref<number>(0);

const selectedParticipant = computed(() =>
  props.participants.find(
    participant => participant.participantId === selectedParticipantId.value
  )
);

const canSubmit = computed(
  () =>
    !props.loading &&
    selectedParticipantId.value > 0 &&
    selectedRewardId.value > 0
);

watch(
  () => props.success,
  value => {
    if (value) {
      selectedParticipantId.value = 0;
      selectedRewardId.value = 0;
    }
  }
);

function submitForm() {
  if (!canSubmit.value) {
    return;
  }

  emit(
    'submit',
    selectedParticipantId.value,
    selectedRewardId.value
  );
}
</script>

<template>
  <div class="redemption-form">
    <h2>New Redemption</h2>

    <div>
      <label for="participant">Participant</label>

      <select
        id="participant"
        v-model="selectedParticipantId"
      >
        <option :value="0">Select participant</option>

        <option
          v-for="participant in participants"
          :key="participant.participantId"
          :value="participant.participantId"
          :disabled="!participant.isActive"
        >
          {{ participant.fullName }}
        </option>
      </select>
    </div>

    <div v-if="selectedParticipant">
      <strong>
        Current points: {{ selectedParticipant.pointsBalance }}
      </strong>
    </div>

    <div>
      <label for="reward">Reward</label>

      <select
        id="reward"
        v-model="selectedRewardId"
      >
        <option :value="0">Select reward</option>

        <option
          v-for="reward in rewards"
          :key="reward.rewardId"
          :value="reward.rewardId"
          :disabled="!reward.isActive"
        >
          {{ reward.name }} - {{ reward.pointsCost }} points
        </option>
      </select>
    </div>

    <button
      type="button"
      :disabled="!canSubmit"
      @click="submitForm"
    >
      {{ loading ? 'Submitting...' : 'Redeem' }}
    </button>

    <p v-if="success" class="success">
      {{ success }}
    </p>

    <p v-if="error" class="error">
      {{ error }}
    </p>
  </div>
</template>