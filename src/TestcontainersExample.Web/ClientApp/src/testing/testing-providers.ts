import {provideHttpClientTesting} from "@angular/common/http/testing";
import {BASE_URL, API_URL} from "../app/injectable-tokens";
import {provideHttpClient} from "@angular/common/http";

export const getTestingProviders = (): unknown[] => [
    { provide: BASE_URL, useValue: '/' },
    { provide: API_URL, useValue: '/api/' },
  provideHttpClient(),
    provideHttpClientTesting()
];
