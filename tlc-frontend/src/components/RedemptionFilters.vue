<script setup lang="ts">
import { RedemptionStatus } from '../types';

defineProps<{
  selectedStatus: RedemptionStatus | '';
  selectedCountry: string;
  countries: string[];
}>();

const emit = defineEmits<{
  statusChange: [value: RedemptionStatus | ''];
  countryChange: [value: string];
}>();
</script>

<template>
  <div class="filters">
    <div>
      <label for="status">Status</label>

      <select
        id="status"
        :value="selectedStatus"
        @change="
          emit(
            'statusChange',
            ($event.target as HTMLSelectElement).value as RedemptionStatus | ''
          )
        "
      >
        <option value="">All</option>
        <option :value="RedemptionStatus.Pending">Pending</option>
        <option :value="RedemptionStatus.Completed">Completed</option>
        <option :value="RedemptionStatus.Failed">Failed</option>
      </select>
    </div>

    <div>
      <label for="country">Country</label>

      <select
        id="country"
        :value="selectedCountry"
        @change="
          emit(
            'countryChange',
            ($event.target as HTMLSelectElement).value
          )
        "
      >
        <option value="">All</option>

        <option
          v-for="country in countries"
          :key="country"
          :value="country"
        >
          {{ country }}
        </option>
      </select>
    </div>
  </div>
</template>