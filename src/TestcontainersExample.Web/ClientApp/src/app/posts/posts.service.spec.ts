import { TestBed } from '@angular/core/testing';

import { PostsService } from './posts.service';
import { HttpTestingController } from '@angular/common/http/testing';
import { createTestService } from '../../testing/testing-service';
import { InjectionToken } from '@angular/core';
import { API_URL } from '../injectable-tokens';

describe('PostsService', () => {
  let service: PostsService;
  let http: HttpTestingController;
  let url: string;

  beforeEach(async () => {
    const fixture = await createTestService(PostsService);
    service = fixture.service;
    http = fixture.getService(HttpTestingController);
    url = fixture.getService<string>(API_URL);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('get', () => {
    it('should call the api with GET method', () => {
      service.getPosts().subscribe();
      const req = http.expectOne(url + '/posts');
      expect(req.request.method).toBe('GET');
      req.flush([]);
      http.verify();
    });
  });

  describe('add', () => {
    it('should call the api with POST method', () => {
      service.addPost({} as any).subscribe();
      const req = http.expectOne(url + '/posts');
      expect(req.request.method).toBe('POST');
      req.flush([]);
      http.verify();
    });
  });
});
