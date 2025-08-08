import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { CreateIssue, Issue } from './types';

export const issuesApi = createApi({
  reducerPath: 'issues-api',
  baseQuery: fetchBaseQuery({
    baseUrl: 'http://localhost:5000',
  }),
  tagTypes: ['issues'],
  endpoints: (builder) => ({
    getIssues: builder.query<Issue[], void>({
      query: () => '/issues',
      providesTags: ['issues'],
    }),
    getIssue: builder.query<Issue, { id: string }>({
      query: ({ id }) => `/issues/${id}`,
    }),
    createIssue: builder.mutation<Issue, CreateIssue>({
      query: (data) => ({ url: '/issues', method: 'POST', body: data }),
      invalidatesTags: ['issues'],
    }),
  }),
});

export const { useGetIssuesQuery, useGetIssueQuery, useCreateIssueMutation } =
  issuesApi;
