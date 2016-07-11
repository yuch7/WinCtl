#include "pebble.h"

static Window *main_win;
static Layer *canvas_layer;


enum msgs {
	vol_up,
	vol_down,
	vol_mute,
	sleep_4000,
	sleep_800
};

static void sel_click_handler(ClickRecognizerRef rec, void * context) {
  
}

static void up_click_handler(ClickRecognizerRef rec, void * context) {
  
}

static void down_click_handler(ClickRecognizerRef rec, void * context) {
  
}

static void click_config_provider(Window *window) {
  ButtonId butt_sel = BUTTON_ID_SELECT;
  ButtonId butt_up = BUTTON_ID_UP;
  ButtonId butt_down = BUTTON_ID_DOWN;
  window_single_click_subscribe(butt_sel, sel_click_handler);
  window_single_click_subscribe(butt_up, up_click_handler);
  window_single_click_subscribe(butt_down, down_click_handler);
  
}

static void canvas_update(Layer *layer, GContext *ctx) {
  GRect bounds = layer_get_bounds(layer);
  GFont font = fonts_get_system_font(FONT_KEY_LECO_32_BOLD_NUMBERS);
  graphics_context_set_text_color(ctx, GColorBlack);
  
  graphics_draw_text(ctx, "+", font, bounds, GTextOverflowModeWordWrap, GTextAlignmentRight, NULL);
}

static void deinit() {
  layer_destroy(canvas_layer);
}

static void main_win_ld(Window * window) {  
  
  Layer *win_layer = window_get_root_layer(window);
  GRect bounds = layer_get_bounds(win_layer);
  canvas_layer = layer_create(bounds);
  
  layer_set_update_proc(canvas_layer, canvas_update);
  layer_add_child(win_layer, canvas_layer);
    
  window_set_click_config_provider(main_win, (ClickConfigProvider) click_config_provider);
  
}

static void main_win_unld(Window * window) {
  layer_destroy(canvas_layer);
}

static void init() {

	main_win = window_create();
	window_set_window_handlers(main_win,
		(WindowHandlers){
			.load = main_win_ld,
			.unload = main_win_unld
		});
  
  window_stack_push(main_win,true);
}

int main(void) {  
	init();
	app_event_loop();
	deinit();
}