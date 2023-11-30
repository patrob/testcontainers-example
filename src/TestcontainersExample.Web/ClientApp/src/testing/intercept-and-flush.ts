import { HttpTestingController } from '@angular/common/http/testing';
import { RenderResult } from '@testing-library/angular';

export class InterceptAndFlush<T> {
    httpController: HttpTestingController;
    component: RenderResult<T, T> | null = null;

    constructor(httpTestingController: HttpTestingController, component: RenderResult<T, T> | null = null) {
        this.httpController = httpTestingController;
        this.component = component;
    }
}
