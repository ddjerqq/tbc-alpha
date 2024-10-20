<script>
  import { page } from "$app/stores";
  import Icon from "heroicons-for-svelte";
  import {Home, SwitchHorizontal, Collection, Chat, Search, Template} from "heroicons-for-svelte/icons/outline";
</script>


<div class="fixed bottom-0 left-0 z-50 w-full h-20 bg-white border-t border-gray-200">
    <div class="grid h-full max-w-lg grid-cols-4 mx-auto font-medium">
        <a href="/" class:active={$page.url.pathname === "/"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Home} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Home</span>
        </a>

        <a href="/payments" class:active={$page.url.pathname === "/payments"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={SwitchHorizontal} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Payments</span>
        </a>

        <a href="/chat" class:active={$page.url.pathname === "/chat"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Chat} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Chatbot</span>
        </a>

        <a href="/searc" class:active={$page.url.pathname === "/searc"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Search} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Search</span>
        </a>
    </div>
</div>

<style>
    a.active {
      color: blue;
    }
</style>