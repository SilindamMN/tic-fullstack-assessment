import axios from 'axios';

import type {
  Redemption,
  Participant,
  Reward,
  CreateRedemptionRequest
} from '../types';

const http = axios.create({
  baseURL: 'https://localhost:7143/api'
});

http.interceptors.response.use(
  response => response,
  error => {
    throw new Error(error.response?.data?.message ?? 'Something went wrong.');
  }
);

export async function getRedemptions(
  status?: string,
  country?: string
): Promise<Redemption[]> {
  const { data } = await http.get<Redemption[]>('/redemptions', {
    params: { status, countryCode: country }
  });

  return data;
}

export async function getParticipants(): Promise<Participant[]> {
  const { data } = await http.get<Participant[]>('/participants');

  return data;
}

export async function getRewards(): Promise<Reward[]> {
  const { data } = await http.get<Reward[]>('/rewards');

  return data;
}

export async function createRedemption(
  request: CreateRedemptionRequest
): Promise<Redemption> {
  const { data } = await http.post<Redemption>('/redemptions', request);

  return data;
}