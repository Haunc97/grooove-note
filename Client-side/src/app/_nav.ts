interface NavAttributes {
  [propName: string]: any;
}
interface NavWrapper {
  attributes: NavAttributes;
  element: string;
}
interface NavBadge {
  text: string;
  variant: string;
}
interface NavLabel {
  class?: string;
  variant: string;
}

export interface NavData {
  name?: string;
  url?: string;
  icon?: string;
  badge?: NavBadge;
  title?: boolean;
  children?: NavData[];
  variant?: string;
  attributes?: NavAttributes;
  divider?: boolean;
  class?: string;
  label?: NavLabel;
  wrapper?: NavWrapper;
}

export const navItems: NavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    icon: 'icon-speedometer',
    badge: {
      variant: 'info',
      text: 'NEW'
    }
  },
  {
    title: true,
    name: 'Categories'
  },
  {
    name: 'Notebooks',
    url: '/notebooks',
    icon: 'icon-notebook'
  },
  {
    name: 'Notes',
    url: '/notes',
    icon: 'icon-note'
  },
  {
    name: 'Chat',
    url: '/chat',
    icon: 'icon-note'
  },
  {
    divider: true
  },
  {
    title: true,
    name: 'Extras',
  }
];
