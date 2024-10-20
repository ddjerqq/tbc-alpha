<script>
  import { page } from "$app/stores";
  import Icon from "heroicons-for-svelte";
  import {Home, SwitchHorizontal, Collection, Template} from "heroicons-for-svelte/icons/outline";
</script>


<div class="fixed bottom-0 left-0 z-50 w-full h-20 bg-white border-t border-gray-200">
    <div class="grid h-full max-w-lg grid-cols-4 mx-auto font-medium">
        <a href="/" class:active={$page.url.pathname === "/"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Home} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Home</span>
        </a>

        <a href="/transactions" class:active={$page.url.pathname === "/transactions"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Collection} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Transactions</span>
        </a>

        <a href="/payments" class:active={$page.url.pathname === "/payments"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={SwitchHorizontal} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Payments</span>
        </a>

        <a href="/hub" class:active={$page.url.pathname === "/hub"} class="p-3 inline-flex flex-col items-center justify-center px-5 hover:bg-gray-50 group">
            <Icon icon={Template} class="text-2xl text-gray-500 group-hover:text-blue-600"/>
            <span class="text-sm text-gray-500 group-hover:text-blue-600">Hub</span>
        </a>
    </div>
</div>

<style>
    a.active {
      color: blue;
    }
</style>