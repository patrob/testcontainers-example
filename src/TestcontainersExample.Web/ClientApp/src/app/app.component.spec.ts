import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { render } from '@testing-library/angular';

describe('AppComponent', () => {
  it('should create the app', async () => {
    const { fixture } = await setup();
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have the 'testcontainers-example' title`, async () => {
    const { fixture } = await setup();
    const app = fixture.componentInstance;
    expect(app.title).toEqual('testcontainers-example');
  });

  it('should render title', async () => {
    const { fixture } = await setup();
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain(
      'Hello, testcontainers-example',
    );
  });

  const setup = async () => await render(AppComponent, {});
});
