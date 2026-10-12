```vue
<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import RedemptionFilters from './components/RedemptionFilters.vue';
import RedemptionForm from './components/RedemptionForm.vue';
import RedemptionTable from './components/RedemptionTable.vue';
import {
  createRedemption,
  getParticipants,
  getRedemptions,
  getRewards,
} from './services/api';
import type {
  Participant,
  Redemption,
  RedemptionStatus,
  Reward
} from './types';

const redemptions = ref<Redemption[]>([]);
const participants = ref<Participant[]>([]);
const rewards = ref<Reward[]>([]);

const selectedStatus = ref<RedemptionStatus | ''>('');
const selectedCountry = ref('');

const page = ref(1);
const pageSize = 7;

const loading = ref(false);
const formLoading = ref(false);
const error = ref('');
const formError = ref('');
const formSuccess = ref('');

const countries = computed(() =>
  [...new Set(participants.value.map(x => x.countryCode))].sort()
);

const totalPages = computed(() =>
  Math.max(1, Math.ceil(redemptions.value.length / pageSize))
);

const pagedRedemptions = computed(() =>
  redemptions.value.slice(
    (page.value - 1) * pageSize,
    page.value * pageSize
  )
);

async function loadRedemptions() {
  loading.value = true;
  error.value = '';

  try {
    redemptions.value = await getRedemptions(
      selectedStatus.value || undefined,
      selectedCountry.value || undefined
    );
  } catch (err) {
    error.value =
      err instanceof Error
        ? err.message
        : 'Failed to load redemptions.';
  } finally {
    loading.value = false;
  }
}

async function loadFormData() {
  try {
    participants.value = await getParticipants();
    rewards.value = await getRewards();
  } catch (err) {
    formError.value =
      err instanceof Error
        ? err.message
        : 'Failed to load participants or rewards.';
  }
}

async function handleCreate(
  participantId: number,
  rewardId: number
) {
  formLoading.value = true;
  formError.value = '';
  formSuccess.value = '';

  try {
    const result = await createRedemption({
      participantId,
      rewardId
    });

    page.value = 1;
    await loadRedemptions();

    participants.value = await getParticipants();

    formSuccess.value = result.message ?? 'Redemption submitted.';
  } catch (err) {
    formError.value =
      err instanceof Error
        ? err.message
        : 'Failed to create redemption.';
  } finally {
    formLoading.value = false;
  }
}

async function handleStatusChange(
  status: RedemptionStatus | ''
) {
  selectedStatus.value = status;
  page.value = 1;
  await loadRedemptions();
}

async function handleCountryChange(country: string) {
  selectedCountry.value = country;
  page.value = 1;
  await loadRedemptions();
}

onMounted(async () => {
  await Promise.all([
    loadRedemptions(),
    loadFormData()
  ]);
});
</script>

<template>
  <main class="container">
    <h1>TIC Redemption System</h1>

    <section>
      <RedemptionForm
        :participants="participants"
        :rewards="rewards"
        :loading="formLoading"
        :error="formError"
        :success="formSuccess"
        @submit="handleCreate"
      />
    </section>

    <section>
      <h2>Redemptions</h2>

      <RedemptionFilters
        :selected-status="selectedStatus"
        :selected-country="selectedCountry"
        :countries="countries"
        @status-change="handleStatusChange"
        @country-change="handleCountryChange"
      />

      <p v-if="loading">
        Loading redemptions...
      </p>

      <p v-else-if="error" class="error">
        {{ error }}
      </p>

      <template v-else>
        <RedemptionTable
          :redemptions="pagedRedemptions"
        />

        <div class="pagination">
          <button
            type="button"
            :disabled="page <= 1"
            @click="page--"
          >
            Prev
          </button>

          <span>Page {{ page }} of {{ totalPages }}</span>

          <button
            type="button"
            :disabled="page >= totalPages"
            @click="page++"
          >
            Next
          </button>
        </div>
      </template>
    </section>
  </main>
</template>
```